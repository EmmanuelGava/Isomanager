using System;
using System.Linq;
using System.Web.UI;
using Isomanager.Models;


namespace Isomanager.Pages
{
    public partial class Contextos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Código para el evento Page_Load
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

                // Asegúrate de que los paneles o secciones estén ocultos al cargar la página inicialmente
                CargarFODA.Visible = false;
                MostrarFODA.Visible = false;

                // Comprobar si hay un ContextoId en la sesión
                int? contextoId = Session["contextoId"] as int?;
                if (contextoId.HasValue)
                {
                    using (var context = new MyDbContext())
                    {
                        // Verificar si ya existe un FODA para este ContextoId
                        var fodaExistente = context.Fodas.FirstOrDefault(f => f.ContextoId == contextoId.Value);
                        if (fodaExistente != null)
                        {
                            // Cargar los datos del FODA en los labels
                            lblFortalezas.Text = fodaExistente.Fortalezas;
                            lblDebilidades.Text = fodaExistente.Debilidades;
                            lblOportunidades.Text = fodaExistente.Oportunidades;
                            lblAmenazas.Text = fodaExistente.Amenazas;

                            // Mostrar el panel de resultados
                            MostrarFODA.Visible = true;
                        }
                    }
                }
            }
        }

        protected void btnFODA_Click(object sender, EventArgs e)
        {
            
            if (CargarFODA.Visible == false)
            {
                CargarFODA.Visible = true;
            }else
            {
                CargarFODA.Visible = false;
            }    
        }

        protected void btnGuardarFODA_Click(object sender, EventArgs e)
        {
            // Guardar el contenido de los TextBox en las etiquetas
            lblFortalezas.Text = txtFortalezas.Text;
            lblDebilidades.Text = txtDebilidades.Text;
            lblOportunidades.Text = txtOportunidades.Text;
            lblAmenazas.Text = txtAmenazas.Text;

            // Asignar el ContextoId desde la sesión
            int? contextoId = Session["contextoId"] as int?;

            if (contextoId.HasValue)
            {
                using (var context = new MyDbContext())
                {
                    // Verificar si el ContextoId existe
                    var contextoExistente = context.Contextos.FirstOrDefault(c => c.ContextoId == contextoId.Value);
                    if (contextoExistente == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error: El Contexto no existe.');", true);
                        return;
                    }

                    var foda = new Foda
                    {
                        ContextoId = contextoId.Value, // Usar el ContextoId de la sesión
                        Fortalezas = txtFortalezas.Text,
                        Debilidades = txtDebilidades.Text,
                        Oportunidades = txtOportunidades.Text,
                        Amenazas = txtAmenazas.Text,
                    };

                    // Verificar si ya existe un FODA para este ContextoId
                    var fodaExistente = context.Fodas.FirstOrDefault(f => f.ContextoId == foda.ContextoId);
                    if (fodaExistente != null)
                    {
                        // Actualizar el FODA existente
                        fodaExistente.Fortalezas = foda.Fortalezas;
                        fodaExistente.Debilidades = foda.Debilidades;
                        fodaExistente.Oportunidades = foda.Oportunidades;
                        fodaExistente.Amenazas = foda.Amenazas;
                    }
                    else
                    {
                        // Agregar el nuevo FODA
                        context.Fodas.Add(foda);
                    }

                    // Guardar los cambios en la base de datos
                    context.SaveChanges();
                }

                // Mostrar mensaje de éxito
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('FODA guardado exitosamente.');", true);
            }
            else
            {
                // Mostrar mensaje de error si no hay ContextoId
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error: No se ha seleccionado un Contexto.');", true);
            }

            // Limpiar los campos de texto después de guardar
            txtFortalezas.Text = "";
            txtDebilidades.Text = "";
            txtOportunidades.Text = "";
            txtAmenazas.Text = "";

            // Ocultar el formulario de FODA y mostrar los resultados
            CargarFODA.Visible = false;
            MostrarFODA.Visible = true;
        }

        protected void btnMapeoProcesos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/MapeoProcesos");
        }

        protected void btnFactoresExternos_Click(object sender, EventArgs e)
        {
            // Código para el botón Factores Externos
        }

        protected void btnAlcanceSistema_Click(object sender, EventArgs e)
        {
            // Código para el botón Alcance del Sistema
        }

        
    }
}
