using System;
using System.Collections.Generic;
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
                CargarProcesosClave();
                CargarProcesosDropDown();
                CargarDocumentacion();
                CargarMejoras();
                CargarRiesgosOportunidades();
            }
        }

        private void CargarProcesosClave()
        {
            var procesosClave = new List<ProcesoClave>
            {
                new ProcesoClave { Nombre = "Compras", Propietario = "Dpto. Compras", Objetivo = "Adquirir insumos de calidad" },
                new ProcesoClave { Nombre = "Producción", Propietario = "Dpto. Producción", Objetivo = "Fabricar productos que cumplan estándares" },
                new ProcesoClave { Nombre = "Ventas", Propietario = "Dpto. Ventas", Objetivo = "Maximizar ingresos y satisfacción del cliente" }
            };
            gvProcesosClave.DataSource = procesosClave;
            gvProcesosClave.DataBind();
        }

        private void CargarProcesosDropDown()
        {
            ddlProcesos.DataSource = new List<string> { "Compras", "Producción", "Ventas" };
            ddlProcesos.DataBind();
        }

        private void CargarDocumentacion()
        {
            var documentos = new List<Documento>
            {
                new Documento { Id = 1, Nombre = "Procedimiento de Compras", Tipo = "Procedimiento" },
                new Documento { Id = 2, Nombre = "Política de Calidad", Tipo = "Política" }
            };
            gvDocumentacion.DataSource = documentos;
            gvDocumentacion.DataBind();
        }

        private void CargarMejoras()
        {
            var mejoras = new List<Mejora>
            {
                new Mejora { Proceso = "Compras", AreaMejora = "Tiempo de procesamiento", AccionRecomendada = "Implementar sistema automatizado", Responsable = "Juan Pérez", FechaImplementacion = DateTime.Now.AddMonths(1) }
            };
            gvMejoras.DataSource = mejoras;
            gvMejoras.DataBind();
        }

        private void CargarRiesgosOportunidades()
        {
            var riesgosOportunidades = new List<RiesgoOportunidad>
            {
                new RiesgoOportunidad { Proceso = "Producción", Tipo = "Riesgo", Descripcion = "Falla de maquinaria", Impacto = "Alto", AccionPropuesta = "Implementar mantenimiento preventivo" },
                new RiesgoOportunidad { Proceso = "Ventas", Tipo = "Oportunidad", Descripcion = "Expansión a nuevos mercados", Impacto = "Alto", AccionPropuesta = "Realizar estudio de mercado" }
            };
            gvRiesgosOportunidades.DataSource = riesgosOportunidades;
            gvRiesgosOportunidades.DataBind();
        }

        protected void ddlProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string procesoSeleccionado = ddlProcesos.SelectedValue;
            lblNombreProceso.Text = procesoSeleccionado;
            lblDescripcionProceso.Text = $"Descripción del proceso de {procesoSeleccionado}";

            blEntradas.Items.Clear();
            blEntradas.Items.Add($"Entrada 1 de {procesoSeleccionado}");
            blEntradas.Items.Add($"Entrada 2 de {procesoSeleccionado}");

            blSalidas.Items.Clear();
            blSalidas.Items.Add($"Salida 1 de {procesoSeleccionado}");
            blSalidas.Items.Add($"Salida 2 de {procesoSeleccionado}");

            blRecursos.Items.Clear();
            blRecursos.Items.Add($"Recurso 1 de {procesoSeleccionado}");
            blRecursos.Items.Add($"Recurso 2 de {procesoSeleccionado}");

            blKPIs.Items.Clear();
            blKPIs.Items.Add($"KPI 1 de {procesoSeleccionado}");
            blKPIs.Items.Add($"KPI 2 de {procesoSeleccionado}");
        }

        protected void btnAgregarProceso_Click(object sender, EventArgs e)
        {
            // Implementar lógica para agregar proceso
        }

        protected void btnEditarDiagrama_Click(object sender, EventArgs e)
        {
            // Implementar lógica para editar diagrama
        }

        protected void btnAgregarMejora_Click(object sender, EventArgs e)
        {
            // Implementar lógica para agregar mejora
        }

        protected void btnAgregarRiesgoOportunidad_Click(object sender, EventArgs e)
        {
            // Implementar lógica para agregar riesgo/oportunidad
        }
    }

    public class ProcesoClave
    {
        public string Nombre { get; set; }
        public string Propietario { get; set; }
        public string Objetivo { get; set; }
    }

    public class Documento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }
    public class RiesgoOportunidad
    {
        public string Proceso { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Impacto { get; set; }
        public string AccionPropuesta { get; set; }
    }

}