using System;
using System.Web.UI;
using Isomanager.Models;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Diagnostics;
using System.Collections.Generic;


namespace Isomanager.Pages
{
    public partial class CrearNorma : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarNormas();

                CargarResponsables();
                lblFechaCreacion.Text = DateTime.Now.ToString("dd/MM/yyyy"); // Formato de fecha
            }
        }

        private void CargarNormas()
        {
            using (var context = new MyDbContext())
            {
                // Asegúrate de incluir el responsable
                var normas = context.Normas.Include(n => n.Responsable).ToList();
                gvNormas.DataSource = normas;
                gvNormas.DataBind(); // Vincular los datos al GridView

                foreach (var norma in normas)
                {
                    Debug.WriteLine($"Norma: {norma.Titulo}, Responsable: {norma.Responsable?.Nombre}");
                }
            }
        }

        private void CargarResponsables()
        {
            using (var context = new MyDbContext())
            {
                // Cargar la lista de usuarios desde la base de datos
                var responsables = context.Usuarios.ToList();

                // Configurar el DropDownList con los datos
                ddlResponsable.DataSource = responsables;
                ddlResponsable.DataTextField = "Nombre";
                ddlResponsable.DataValueField = "UsuarioId";
                ddlResponsable.DataBind();

                // Agregar la opción "Seleccione un responsable" como la primera opción
                ddlResponsable.Items.Insert(0, new ListItem("Seleccione un responsable", ""));

                // Agregar la opción "Agregar Nuevo Usuario" como la última opción
                ddlResponsable.Items.Add(new ListItem("Agregar Nuevo Usuario", "nuevo"));
            }
        }




        protected void gvNormas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int normaId = Convert.ToInt32(e.CommandArgument);
                CargarNormaParaEdicion(normaId);
            }
            else if (e.CommandName == "Delete")
            {
                int normaId = Convert.ToInt32(e.CommandArgument);
                EliminarNorma(normaId);
            }
        }

        // Método para cargar la norma en el formulario para editar
        private void CargarNormaParaEdicion(int normaId)
        {
            using (var context = new MyDbContext())
            {
                var norma = context.Normas.Include(n => n.Responsable).FirstOrDefault(n => n.NormaId == normaId);
                if (norma != null)
                {
                    Titulo.Text = norma.Titulo;
                    Version.Text = norma.Version;
                    Estado.SelectedValue = norma.Estado;
                    lblFechaCreacion.Text = norma.FechaCreacion.ToString("dd/MM/yyyy");
                    ddlResponsable.SelectedValue = norma.ResponsableId.ToString(); // Seleccionar el responsable
                    ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#modalAgregarUsuario').modal('show');", true);
                }
            }
        }

        // Método para eliminar la norma
        private void EliminarNorma(int normaId)
        {
            using (var context = new MyDbContext())
            {
                var norma = context.Normas.Find(normaId);
                if (norma != null)
                {
                    context.Normas.Remove(norma);
                    context.SaveChanges();
                    CargarNormas(); // Recargar la lista de normas después de eliminar
                }
            }
        }




        protected void ddlResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlResponsable.SelectedValue == "nuevo")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#modalAgregarUsuario').modal('show');", true);
            }
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txtNombre.Text;
            string nuevoEmail = txtEmail.Text;
            string nuevoRol = ddlRol.SelectedValue;

            // Verificar si la validación pasó
            if (Page.IsValid)
            {
                try
                {
                    // Crear el nuevo usuario
                    var nuevoUsuario = UsuarioHelper.CrearUsuario(nuevoNombre, nuevoEmail, nuevoRol);

                    // Limpiar los controles después de guardar
                    txtNombre.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    ddlRol.SelectedIndex = 0;

                    // Cerrar el modal y actualizar la lista
                    ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#modalAgregarUsuario').modal('hide');", true);
                    CargarResponsables();
                }
                catch (InvalidOperationException ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{ex.Message}');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error: {ex.Message}');", true);
                }
            }
            else
            {
                // Si la validación no pasó, actualiza el UpdatePanel para mostrar los errores
                ScriptManager.RegisterStartupScript(this, GetType(), "updatePanel", "Sys.Application.repaint();", true);
            }
        }


        protected void btnGuardarNorma_Click(object sender, EventArgs e)
        {
            string nombreNorma = Titulo.Text;  // Nombre de la norma
            string versionNorma = Version.Text;  // Versión de la norma
            string estadoNorma = Estado.SelectedValue;  // Estado de la norma
            DateTime fechaCreacion;

            // Intentar parsear la fecha de creación desde el Label
            if (!DateTime.TryParse(lblFechaCreacion.Text, out fechaCreacion))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('La fecha de creación no es válida.');", true);
                return;
            }

            // Verificar si el responsable está seleccionado
            int? responsableId = !string.IsNullOrEmpty(ddlResponsable.SelectedValue) ?
                int.Parse(ddlResponsable.SelectedValue) : (int?)null;

            if (string.IsNullOrEmpty(nombreNorma) || string.IsNullOrEmpty(versionNorma) || responsableId == null)
            {
                // Mostrar mensaje de error si los campos no están completos
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Todos los campos son obligatorios.');", true);
                return;
            }

            try
            {
                using (var context = new MyDbContext())
                {
                    // Verificar si se está editando una norma existente
                    var norma = context.Normas.FirstOrDefault(n => n.Titulo == nombreNorma); // Cambia esto según tu lógica
                    if (norma != null)
                    {
                        // Editar la norma existente
                        norma.Version = versionNorma;
                        norma.Estado = estadoNorma;
                        norma.FechaCreacion = fechaCreacion;
                        norma.ResponsableId = responsableId.Value;
                    }
                    else
                    {
                        // Crear una nueva norma
                        norma = new Norma
                        {
                            Titulo = nombreNorma,
                            Version = versionNorma,
                            Estado = estadoNorma,
                            FechaCreacion = fechaCreacion,
                            ResponsableId = responsableId.Value,
                            Contextos = new List<Contexto>() // Inicializa la colección de contextos
                        };

                        // Crear un nuevo contexto asociado
                        var nuevoContexto = new Contexto
                        {
                            // Asigna las propiedades necesarias para el contexto
                            // Por ejemplo:
                            Norma = norma, // Asocia el contexto a la norma
                                           // Otras propiedades del contexto...
                        };

                        // Agregar el contexto a la norma
                        norma.Contextos.Add(nuevoContexto);
                        context.Normas.Add(norma);
                    }

                    context.SaveChanges();
                    CargarNormas(); // Recargar la lista de normas después de guardar
                    LimpiarCampos(); // Método para limpiar los campos del formulario
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Norma guardada exitosamente.');", true);
                }
            }
            catch (Exception ex)
            {
                // Manejar errores generales al guardar la norma
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Error al guardar la norma: {ex.Message}');", true);
            }
        }

        // Método para limpiar los campos del formulario
        private void LimpiarCampos()
        {
            Titulo.Text = string.Empty;
            Version.Text = string.Empty;
            Estado.SelectedIndex = 0;
            ddlResponsable.SelectedIndex = 0;
            lblFechaCreacion.Text = DateTime.Now.ToString("dd/MM/yyyy"); // Resetear la fecha
        }


    }

}
