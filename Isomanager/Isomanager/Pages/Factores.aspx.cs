// path/to/your/project/Factores.aspx.cs
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Isomanager.Models;

namespace Isomanager.Pages
{
    
    public partial class Factores : System.Web.UI.Page
    {
        private MyDbContext db = new MyDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTiposFactores(); // Cargar tipos de factores desde la base de datos si es necesario
            }
        }
        private const int limiteOpciones = 12;


        private void LoadTiposFactores()
        {
            ddlTipoFactor.Items.Clear();

            ddlTipoFactor.Items.Add(new ListItem("Seleccione un tipo", ""));
            ddlTipoFactor.Items.Add(new ListItem("Económico", "Económico"));
            ddlTipoFactor.Items.Add(new ListItem("Político", "Político"));
            ddlTipoFactor.Items.Add(new ListItem("Social", "Social"));
            ddlTipoFactor.Items.Add(new ListItem("Tecnológico", "Tecnológico"));
            ddlTipoFactor.Items.Add(new ListItem("Ambiental", "Ambiental"));
            ddlTipoFactor.Items.Add(new ListItem("Legal", "Legal"));
            

            var tipos = db.TiposFactores.ToList();
            foreach (var tipo in tipos)
            {
                ddlTipoFactor.Items.Add(new ListItem(tipo.Nombre, tipo.Id.ToString()));
            }
            ddlTipoFactor.Items.Add(new ListItem("Agregar nuevo factor", "nuevo"));
        }

        protected void factorForm_Submit(object sender, EventArgs e)
        {
            try
            {
                var newFactor = new FactoresExternos
                {
                    ContextoId = (int)Session["ContextoId"],
                    Descripcion = descripcion.Value,
                    FechaCreacion = DateTime.Parse(fechaCreacion.Value),
                    Impacto = impacto.SelectedValue,
                    Probabilidad = probabilidad.SelectedValue,
                    AccionesSugeridas = accionesSugeridas.Value,
                    Responsable = responsable.Value,
                    TipoFactorId = int.Parse(ddlTipoFactor.SelectedValue)
                };

                db.FactoresExternos.Add(newFactor);
                db.SaveChanges();
                Response.Redirect("~/Pages/Contextos.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
            }
           
        }

        protected void tipoFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFactor.SelectedValue == "nuevo")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "mostralModal", "$('#modalNuevoFactor').modal('show');", true);


                // Restablece la selección del DropDownList para evitar que siempre abra el modal
                ddlTipoFactor.ClearSelection();
            }
        }

        protected void btnGuardarNuevoFactor_Click(object sender, EventArgs e)
        {
            if (ddlTipoFactor.Items.Count - 1 < limiteOpciones)
            {



                if (!string.IsNullOrEmpty(txtNuevoFactor.Text))
                {
                    // Guardar el nuevo tipo en la base de datos
                    var nuevoTipo = new TipoFactor { Nombre = txtNuevoFactor.Text };
                    db.TiposFactores.Add(nuevoTipo);
                    db.SaveChanges();

                    // Recargar los tipos de factores
                    LoadTiposFactores();

                    // Seleccionar el nuevo tipo de factor en el DropDownList
                    ddlTipoFactor.SelectedValue = nuevoTipo.Id.ToString();

                    // Cerrar el modal y actualizar el UpdatePanel
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModal", "$('#modalNuevoFactor').modal('hide'); $('.modal-backdrop').remove();", true);

                    UpdatePanel1.Update();
                }
            }
            else
            {
                // Mostrar mensaje de advertencia si se alcanza el límite
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Has alcanzado el límite de tipos de factores.');", true);
            }
        }



        protected void btnEliminarFactor_Click(object sender, EventArgs e)
        {
            try
            {
                // Asegurarse de que haya una opción seleccionada y no sea el valor "Agregar nuevo factor"
                if (!string.IsNullOrEmpty(ddlTipoFactor.SelectedValue) && ddlTipoFactor.SelectedValue != "nuevo")
                {
                    int tipoFactorId = int.Parse(ddlTipoFactor.SelectedValue);

                    // Buscar el tipo de factor en la base de datos
                    var tipoFactor = db.TiposFactores.FirstOrDefault(t => t.Id == tipoFactorId);
                    if (tipoFactor != null)
                    {
                        // Eliminar el tipo de factor de la base de datos
                        db.TiposFactores.Remove(tipoFactor);
                        db.SaveChanges();

                        // Recargar el DropDownList para actualizar los cambios
                        LoadTiposFactores();

                        // Mostrar mensaje de éxito
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Tipo de factor eliminado exitosamente.');", true);
                    }
                    else
                    {
                        // Mostrar mensaje de error si el tipo de factor no se encuentra
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Tipo de factor no encontrado.');", true);
                    }
                }
                else
                {
                    // Mostrar mensaje de error si no hay opción seleccionada
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Seleccione un tipo de factor válido para eliminar.');", true);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
            }
        }

    }
}
