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

            // Asignar el NormaId desde la sesión
            int? normaId = Session["NormaId"] as int?;

            if (normaId.HasValue)
            {
                using (var context = new MyDbContext())
                {
                    var foda = new Foda
                    {
                        NormaId = normaId.Value,
                        Fortalezas = txtFortalezas.Text,
                        Debilidades = txtDebilidades.Text,
                        Oportunidades = txtOportunidades.Text,
                        Amenazas = txtAmenazas.Text,
                        
                    };

                    // Verificar si ya existe un FODA para esta Norma
                    var fodaExistente = context.Fodas.FirstOrDefault(f => f.NormaId == foda.NormaId);
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
                // Mostrar mensaje de error si no hay NormaId
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error: No se ha seleccionado una Norma.');", true);
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
