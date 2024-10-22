using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
                CargarMejoras();
                CargarProcesosClave();
                CargarProcesosDropDown();
                CargarCambiosYAuditorias();
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
                    // Mostrar detalles del error, incluyendo la excepción interna si la hay
                    if (ex.InnerException != null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.InnerException.Message}');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
                    }
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
                if (ex.InnerException != null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.InnerException.Message}');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
                }
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
            using (var context = new MyDbContext())
            {
                var procesos = context.Procesos.ToList(); // Obtener todos los procesos de la base de datos

                ddlEvalProcess.DataSource = procesos; // Asignar la lista de procesos como fuente de datos
                ddlEvalProcess.DataTextField = "Nombre"; // Campo que se mostrará en el DropDownList
                ddlEvalProcess.DataValueField = "ProcesoId"; // Campo que se usará como valor
                ddlEvalProcess.DataBind(); // Llenar el DropDownList
                
                ddlProcesoCambio.DataSource = procesos;
                ddlProcesoCambio.DataBind();

                ddlProcesoAuditoria.DataSource = procesos;
                ddlProcesoAuditoria.DataBind();

               

                // Agregar una opción por defecto
                ddlEvalProcess.Items.Insert(0, new ListItem("Seleccione un Proceso", "0"));
                ddlProcesoCambio.Items.Insert(0, new ListItem("Seleccione un Proceso", "0"));
                ddlProcesoAuditoria.Items.Insert(0, new ListItem("Seleccione un Proceso", "0"));
              
            }
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
            catch (DbUpdateException dbEx)
            {
                // Mostrar detalles de la excepción interna
                string mensajeError = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error al guardar el proceso: {mensajeError}');", true);
            }
        }

        protected void btnAgregarMejora_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text.Trim();
            string areaMejora = txtAreaMejora.Text.Trim();
            string accionRecomendada = txtAccionRecomendada.Text.Trim();
            string responsable = txtResponsable.Text.Trim();
            DateTime? fechaImplementacion = string.IsNullOrEmpty(txtFechaImplementacion.Text) ? (DateTime?)null : DateTime.Parse(txtFechaImplementacion.Text);

            if (string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(areaMejora) || string.IsNullOrEmpty(accionRecomendada) || string.IsNullOrEmpty(responsable))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Todos los campos son obligatorios.');", true);
                return;
            }

            try
            {
                using (var context = new MyDbContext())
                {
                    if (Session["ProcesoId"] == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se ha seleccionado un proceso.');", true);
                        return;
                    }

                    var nuevaMejora = new MejoraProceso
                    {
                        Descripcion = descripcion,
                        Fecha = DateTime.Now,
                        ProcesoId = (int)Session["ProcesoId"], // Asegúrate de que este ID esté disponible
                                                               // UsuarioId = (int?)Session["UsuarioId"], // Asigna el ID del usuario que sugiere la mejora
                        AreaMejora = areaMejora,
                        AccionRecomendada = accionRecomendada,
                        Responsable = responsable,
                        FechaImplementacion = fechaImplementacion
                    };

                    context.MejoraProcesos.Add(nuevaMejora);
                    context.SaveChanges();
                }

                // Limpiar campos después de agregar
                txtDescripcion.Text = "";
                txtAreaMejora.Text = "";
                txtAccionRecomendada.Text = "";
                // txtResponsable.Text = ""; // Si no es necesario, puedes dejarlo comentado
                txtFechaImplementacion.Text = "";

                // Actualizar la lista de mejoras si es necesario
                CargarMejoras();
            }
            catch (DbUpdateException dbEx)
            {
                // Mostrar detalles de la excepción interna
                string mensajeError = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error al guardar la mejora: {mensajeError}');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
            }
        }

        private void CargarMejoras()
        {
            if (Session["contextoId"] == null)
            {
                return; // No hay contexto seleccionado
            }

            int contextoId = (int)Session["contextoId"];

            using (var context = new MyDbContext())
            {
                // Cargar todas las mejoras asociadas a los procesos que pertenecen al contexto
                var mejoras = context.MejoraProcesos
                    .Where(m => m.Proceso.ContextoId == contextoId) // Filtrar mejoras por el contexto
                    .ToList();

                gvMejoras.DataSource = mejoras;
                gvMejoras.DataBind();
            }
        }

        public void btnSaveEvaluation_Click(object sender, EventArgs e)
        {
            string procesoSeleccionado = ddlEvalProcess.SelectedItem.Text;
            // string sugerencias = txtImprovements.Text;

            // Lógica para guardar la evaluación en la base de datos (pendiente)
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Evaluación guardada para el proceso: {procesoSeleccionado}');", true);
        }

     

        protected void ddlEvalProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado un proceso válido
            if (ddlEvalProcess.SelectedValue != "0") // Asegúrate de que no sea la opción por defecto
            {
                // Almacenar el ID del proceso en la sesión
                Session["ProcesoId"] = Convert.ToInt32(ddlEvalProcess.SelectedValue);

                // Cargar las mejoras para el proceso seleccionado
                CargarMejoras();
            }
            else
            {
                // Si se selecciona la opción por defecto, puedes limpiar las mejoras
                Session["ProcesoId"] = null; // Limpiar el ProcesoId en la sesión
                gvMejoras.DataSource = null; // Limpiar el GridView
                gvMejoras.DataBind(); // Volver a enlazar el GridView
            }
        }
        protected void ddlProcesoCambio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int procesoId;
            if (int.TryParse(ddlProcesoCambio.SelectedValue, out procesoId))
            {
                // Mensaje de depuración para verificar el ProcesoId seleccionado
                Console.WriteLine($"Proceso seleccionado: {procesoId}");
                CargarMejorasPorProceso(procesoId);
            }
            else
            {
                // Mensaje de depuración si no se pudo convertir el valor
                Console.WriteLine("No se pudo convertir el ProcesoId seleccionado.");
            }
        }

        private void CargarMejorasPorProceso(int procesoId)
        {
            using (var context = new MyDbContext())
            {
                // Mensaje de depuración antes de realizar la consulta
                Console.WriteLine($"Cargando mejoras para el ProcesoId: {procesoId}");

                var mejoras = context.MejoraProcesos
                    .Where(m => m.ProcesoId == procesoId) // Filtrar mejoras por el proceso seleccionado
                    .ToList();

                // Mensaje de depuración para verificar cuántas mejoras se encontraron
                Console.WriteLine($"Número de mejoras encontradas: {mejoras.Count}");

                ddlMejoraCambio.DataSource = mejoras;
                ddlMejoraCambio.DataBind();

                // Agregar una opción por defecto
                ddlMejoraCambio.Items.Insert(0, new ListItem("Seleccione una Mejora", "0"));
            }
        }
        private void CargarCambiosYAuditorias()
        {
            // Obtener el ID del contexto de la sesión
            int? contextoId = Session["ContextoId"] as int?;

            if (contextoId == null)
            {
                // Manejar el caso en que no hay un ContextoId en la sesión
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se ha seleccionado un contexto.');", true);
                return;
            }

            using (var context = new MyDbContext())
            {
                // Consulta a la tabla de CambioProceso, incluyendo la relación con Proceso  
                var cambios = context.CambioProcesos
                   .Include("Proceso")  // Incluir la relación con Proceso  
                   .Where(cp => cp.Proceso.ContextoId == contextoId) // Filtrar por el ContextoId  
                   .Select(cp => new
                   {
                       cp.Descripcion,
                       cp.Responsable,
                       cp.FechaCambio,
                       ProcesoNombre = cp.Proceso.Nombre
                   })
                   .ToList();

                // Asignar los datos al GridView
                gvCambios.DataSource = cambios;
                gvCambios.DataBind();


                // Cargar auditorías relacionadas con el contexto
                var auditorias = context.AuditoriaInternaProcesos
                    .Include("Proceso") // Incluir la relación con Proceso
                    .Where(a => a.Proceso.ContextoId == contextoId) // Filtrar por el ContextoId
                    .Select(a => new
                    {
                        a.FechaAuditoria,
                        a.Responsable,
                        a.Comentarios,
                        ProcesoNombre = a.Proceso.Nombre
                    })
                    .ToList();

                gvAuditorias.DataSource = auditorias;
                gvAuditorias.DataBind();
            }
        }

        protected void btnGuardarCambio_Click(object sender, EventArgs e)
        {
            // Obtener el ID de la mejora seleccionada
            int mejoraId = int.Parse(ddlMejoraCambio.SelectedValue);
            string descripcionCambio = txtDescripcionCambio.Text.Trim();
            string responsableCambio = txtResponsableCambio.Text.Trim();
            DateTime fechaCambio = DateTime.Parse(txtFechaCambio.Text);
            int procesoId = int.Parse(ddlProcesoCambio.SelectedValue); // Obtener el ID del proceso del DropDownList

            if (mejoraId == 0 || string.IsNullOrEmpty(descripcionCambio) || string.IsNullOrEmpty(responsableCambio) || procesoId == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Todos los campos son obligatorios.');", true);
                return;
            }

            try
            {
                using (var context = new MyDbContext())
                {
                    var nuevoCambio = new CambioProceso
                    {
                        CambioId = mejoraId, // Asignar el ID de la mejora seleccionada
                        Descripcion = descripcionCambio,
                        Responsable = responsableCambio,
                        FechaCambio = fechaCambio,
                        ProcesoId = procesoId,
                        //UsuarioId = (int?)Session["UsuarioId"] // Asigna el ID del usuario que realiza el cambio
                    };

                    context.CambioProcesos.Add(nuevoCambio);
                    context.SaveChanges();
                }

                // Limpiar campos después de agregar
                ddlMejoraCambio.SelectedIndex = 0; // Reiniciar el DropDownList de mejoras
                txtDescripcionCambio.Text = "";
                txtResponsableCambio.Text = "";
                txtFechaCambio.Text = "";
                ddlProcesoCambio.SelectedIndex = 0; // Reiniciar el DropDownList de procesos

                // Recargar los cambios y auditorías
                CargarCambiosYAuditorias();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
            }
        }
        protected void btnProgramarAuditoria_Click(object sender, EventArgs e)
        {
            string responsableAuditoria = txtResponsableAuditoria.Text.Trim();
            DateTime fechaAuditoria;
            if (!DateTime.TryParse(txtFechaAuditoria.Text, out fechaAuditoria))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fecha de auditoría no válida.');", true);
                return;
            }

            int procesoId;
            if (!int.TryParse(ddlProcesoAuditoria.SelectedValue, out procesoId) || procesoId == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Todos los campos son obligatorios.');", true);
                return;
            }

            string comentarios = txtComentarios.Text;

            try
            {
                using (var context = new MyDbContext())
                {
                    var nuevaAuditoria = new AuditoriaInternaProceso
                    {
                        FechaAuditoria = fechaAuditoria,
                        Responsable = responsableAuditoria,
                        ProcesoId = procesoId,
                        Comentarios = comentarios,
                        UsuarioId = null // O asignar el ID del auditor si está disponible
                    };

                    context.AuditoriaInternaProcesos.Add(nuevaAuditoria);
                    context.SaveChanges();
                }

                // Limpiar campos después de agregar
                txtFechaAuditoria.Text = "";
                txtResponsableAuditoria.Text = "";
                ddlProcesoAuditoria.SelectedIndex = 0; // Reiniciar el DropDownList

                // Recargar los cambios y auditorías
                CargarCambiosYAuditorias();
            }
            catch (DbUpdateException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error al guardar la auditoría: {ex.InnerException?.Message}');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Ocurrió un error: {ex.Message}');", true);
            }
        }
    }
}
    