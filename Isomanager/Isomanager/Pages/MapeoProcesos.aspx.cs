using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Isomanager.Models;

namespace Isomanager.Pages
{
    public partial class MapeoProcesos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NormaId"] != null)
                {
                    int normaId = (int)Session["NormaId"];
                    using (var context = new MyDbContext())
                    {
                        var norma = context.Normas.FirstOrDefault(n => n.NormaId == normaId);
                        if (norma != null)
                        {
                            lblNormaActual.Text = $"Norma: {norma.Titulo}";
                        }
                    }
                }
                else
                {
                    lblNormaActual.Text = "No se ha seleccionado una norma";
                }

                CargarProcesosClave();
                CargarProcesosDropDown();
            }
        }

        private void CargarProcesosClave()
        {
            if (Session["contextoId"] == null)
            {
                lblNormaActual.Text = "No se ha seleccionado un contexto válido.";
                return;
            }

            int contextoId = (int)Session["contextoId"]; // Validar que contextoId exista

            using (var context = new MyDbContext())
            {
                try
                {
                    // Obtener procesos desde la base de datos
                    var procesosClave = context.Procesos
                        .Where(p => p.ContextoId == contextoId)
                        .ToList();

                    // Crear ejemplos estáticos para mostrar si no hay datos en la base
                    var ejemplosEstaticos = new List<Proceso>
                    {
                        new Proceso { Nombre = "Gestión de Calidad", Propietario = "Equipo de Calidad", Objetivo = "Asegurar la calidad de los productos y servicios", ContextoId = contextoId },
                        new Proceso { Nombre = "Producción", Propietario = "Dpto. de Producción", Objetivo = "Fabricar productos que cumplan los estándares", ContextoId = contextoId },
                        new Proceso { Nombre = "Ventas", Propietario = "Dpto. de Ventas", Objetivo = "Maximizar ingresos y satisfacción del cliente", ContextoId = contextoId }
                    };

                    // Combinar los datos de la base con los ejemplos estáticos
                    procesosClave.AddRange(ejemplosEstaticos);

                    // Asignar la lista al GridView
                    gvProcesosClave.DataSource = procesosClave;
                    gvProcesosClave.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error al cargar los procesos: {ex.Message}');", true);
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btnEdit = (Button)sender;
            int procesoId = Convert.ToInt32(btnEdit.CommandArgument);

            using (var context = new MyDbContext())
            {
                var proceso = context.Procesos.FirstOrDefault(p => p.ProcesoId == procesoId);
                if (proceso != null)
                {
                    txtEditNombre.Text = proceso.Nombre;
                    txtEditPropietario.Text = proceso.Propietario;
                    txtEditObjetivo.Text = proceso.Objetivo;

                    Session["EditProcesoId"] = procesoId;

                    // Mostrar el modal de edición
                    ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", "$('#editProcessModal').modal('show');", true);
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string nombre = txtEditNombre.Text.Trim();
            string propietario = txtEditPropietario.Text.Trim();
            string objetivo = txtEditObjetivo.Text.Trim();

            // Validación de campos obligatorios
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(propietario) || string.IsNullOrEmpty(objetivo))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Todos los campos son obligatorios.');", true);
                return;
            }

            try
            {
                using (var context = new MyDbContext())
                {
                    if (Session["EditProcesoId"] != null)
                    {
                        int procesoId = (int)Session["EditProcesoId"];
                        var proceso = context.Procesos.FirstOrDefault(p => p.ProcesoId == procesoId);
                        if (proceso != null)
                        {
                            proceso.Nombre = nombre;
                            proceso.Propietario = propietario;
                            proceso.Objetivo = objetivo;

                            context.SaveChanges();
                        }
                    }

                    Session.Remove("EditProcesoId");
                    CargarProcesosClave();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int procesoId = Convert.ToInt32(btnDelete.CommandArgument);

            using (var context = new MyDbContext())
            {
                var proceso = context.Procesos.FirstOrDefault(p => p.ProcesoId == procesoId);
                if (proceso != null)
                {
                    context.Procesos.Remove(proceso);
                    context.SaveChanges();
                    CargarProcesosClave();
                }
            }
        }

        private void CargarProcesosDropDown()
        {
            ddlEvalProcess.Items.Clear();
            ddlEvalProcess.Items.Add(new ListItem("Gestión de Calidad", "1"));
            ddlEvalProcess.Items.Add(new ListItem("Producción", "2"));
            ddlEvalProcess.Items.Add(new ListItem("Ventas", "3"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string propietario = txtPropietario.Text.Trim();
            string objetivo = txtObjetivo.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(propietario) || string.IsNullOrEmpty(objetivo))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Todos los campos son obligatorios.');", true);
                return;
            }

            try
            {
                using (var context = new MyDbContext())
                {
                    if (Session["EditProcesoId"] != null)
                    {
                        int procesoId = (int)Session["EditProcesoId"];
                        var proceso = context.Procesos.FirstOrDefault(p => p.ProcesoId == procesoId);
                        if (proceso != null)
                        {
                            proceso.Nombre = nombre;
                            proceso.Propietario = propietario;
                            proceso.Objetivo = objetivo;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        var nuevoProceso = new Proceso
                        {
                            Nombre = nombre,
                            Propietario = propietario,
                            Objetivo = objetivo,
                            ContextoId = (int)Session["contextoId"]
                        };

                        context.Procesos.Add(nuevoProceso);
                        context.SaveChanges();
                    }

                    Session.Remove("EditProcesoId");
                    CargarProcesosClave();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
            }
        }

        public void btnSaveEvaluation_Click(object sender, EventArgs e)
        {
            string procesoSeleccionado = ddlEvalProcess.SelectedItem.Text;
            string sugerencias = txtImprovements.Text;

            // Lógica para guardar la evaluación en la base de datos (pendiente)
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Evaluación guardada para el proceso: {procesoSeleccionado}');", true);
        }

        protected void btnProgramarAuditoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("programarauditoria.aspx");
        }
    }
}