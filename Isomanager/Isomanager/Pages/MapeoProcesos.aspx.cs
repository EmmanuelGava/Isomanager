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
                    //CargarCambios();
                }
            }


            private void CargarProcesosClave()
            {
                // Obtener el ContextoId de la sesión (o de otra fuente)
                int contextoId = (int)Session["contextoId"]; // Asegúrate de que este valor sea válido

                using (var context = new MyDbContext())
                {
                    // Cargar solo los procesos que pertenecen al contexto específico
                    var procesosClave = context.Procesos
                        .Where(p => p.ContextoId == contextoId) // Filtrar por ContextoId
                        .ToList(); // Recuperar los procesos filtrados

                    // Crear ejemplos estáticos
                    var ejemplosEstaticos = new List<Proceso>
            {
                new Proceso { Nombre = "Gestión de Calidad", Propietario = "Equipo de Calidad", Objetivo = "Asegurar la calidad de los productos y servicios", ContextoId = contextoId },
                new Proceso { Nombre = "Producción", Propietario = "Dpto. de Producción", Objetivo = "Fabricar productos que cumplan los estándares", ContextoId = contextoId },
                new Proceso { Nombre = "Ventas", Propietario = "Dpto. de Ventas", Objetivo = "Maximizar ingresos y satisfacción del cliente", ContextoId = contextoId }
            };

                    // Combinar los procesos de la base de datos con los ejemplos estáticos
                    procesosClave.AddRange(ejemplosEstaticos);

                    // Asignar la lista combinada al DataSource del GridView
                    gvProcesosClave.DataSource = procesosClave;
                    gvProcesosClave.DataBind();
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
                    // Limpiar el estado de validación
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal", "$('#editProcessModal').modal('show');", true);
                }
            }
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string nombre = txtEditNombre.Text.Trim();
            string propietario = txtEditPropietario.Text.Trim();
            string objetivo = txtEditObjetivo.Text.Trim();

            // Validar los campos
            if (string.IsNullOrEmpty(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El nombre es obligatorio.');", true);
                return;
            }

            if (string.IsNullOrEmpty(propietario))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El propietario es obligatorio.');", true);
                return;
            }

            if (string.IsNullOrEmpty(objetivo))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El objetivo es obligatorio.');", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ocurrió un error: " + ex.Message + "');", true);
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

            //private void cargarcambios()
            //{
            //    var cambios = new list<cambioauditoria>
            //    {
            //        new cambioauditoria { fecha = datetime.parse("2023-05-15"), proceso = "gestión de calidad", cambio = "actualización de kpis", responsable = "juan pérez" }
            //    };
            //    gvcambios.datasource = cambios;
            //    gvcambios.databind();
            //}

            protected void btnGuardar_Click(object sender, EventArgs e)
            {
                string nombre = txtNombre.Text.Trim();
                string propietario = txtPropietario.Text.Trim();
                string objetivo = txtObjetivo.Text.Trim();

                try
                {
                    using (var context = new MyDbContext())
                    {
                        // Verificar si se está editando un proceso existente
                        if (Session["EditProcesoId"] != null)
                        {
                            int procesoId = (int)Session["EditProcesoId"];
                            var proceso = context.Procesos.FirstOrDefault(p => p.ProcesoId == procesoId);
                            if (proceso != null)
                            {
                                // Actualizar los campos del proceso
                                proceso.Nombre = nombre;
                                proceso.Propietario = propietario;
                                proceso.Objetivo = objetivo;

                                // Guardar cambios
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            // Crear un nuevo proceso
                            var nuevoProceso = new Proceso
                            {
                                Nombre = nombre,
                                Propietario = propietario,
                                Objetivo = objetivo,
                                ContextoId = (int)Session["contextoId"] // Asegúrate de que este valor sea válido
                            };

                            context.Procesos.Add(nuevoProceso);
                            context.SaveChanges();
                        }

                        // Limpiar la sesión
                        Session.Remove("EditProcesoId");

                        // Recargar los procesos
                        CargarProcesosClave();
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ocurrió un error: " + ex.Message + "');", true);
                }
            }







            public void btnSaveEvaluation_Click(object sender, EventArgs e)
            {
                string procesoseleccionado = ddlEvalProcess.SelectedItem.Text;
                string sugerencias = txtImprovements.Text;

                // aquí implementarías la lógica para guardar la evaluación en la base de datos
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('evaluación guardada para el proceso: " + procesoseleccionado + "');", true);
            }

            protected void btnProgramarAuditoria_Click(object sender, EventArgs e)
            {
                Response.Redirect("programarauditoria.aspx");
            }
        }



    }
