using System;
using System.Linq;
using System.Web.UI;
using Isomanager.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace Isomanager.Pages
{
    public partial class Contextos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("Inicio de Page_Load - ContextoId en sesión: " + Session["ContextoId"]);

            if (!IsPostBack)
            {
                if (Session["ContextoId"] != null && int.TryParse(Session["ContextoId"].ToString(), out int contextoId))
                {
                    using (var context = new MyDbContext())
                    {
                        var contexto = context.Contextos.Include(c => c.Norma)
                                                         .FirstOrDefault(c => c.ContextoId == contextoId);

                        if (contexto != null)
                        {
                            lblNormaActual.Text = $"Norma: {contexto.Norma.Titulo}";
                            Debug.WriteLine("Norma encontrada: " + contexto.Norma.Titulo);

                            var fodaExistente = context.Fodas.FirstOrDefault(f => f.ContextoId == contextoId);
                            if (fodaExistente != null)
                            {
                                lblFortalezas.Text = fodaExistente.Fortalezas;
                                lblDebilidades.Text = fodaExistente.Debilidades;
                                lblOportunidades.Text = fodaExistente.Oportunidades;
                                lblAmenazas.Text = fodaExistente.Amenazas;

                                MostrarFODA.Visible = true;
                                Debug.WriteLine("FODA encontrado y cargado en etiquetas.");
                            }
                        }
                        else
                        {
                            lblNormaActual.Text = "No se ha encontrado el contexto.";
                            Debug.WriteLine("Contexto no encontrado para el ContextoId: " + contextoId);
                        }
                    }
                }
                else
                {
                    lblNormaActual.Text = "No se ha seleccionado un contexto.";
                    Debug.WriteLine("Error: ContextoId en sesión no es válido o no es un entero.");
                }

                CargarFODA.Visible = false;
            }
        }

        protected void btnFODA_Click(object sender, EventArgs e)
        {
            CargarFODA.Visible = !CargarFODA.Visible;
            Debug.WriteLine("btnFODA_Click - CargarFODA.Visible cambiado a: " + CargarFODA.Visible);
        }

        protected void btnGuardarFODA_Click(object sender, EventArgs e)
        {
            lblFortalezas.Text = txtFortalezas.Text;
            lblDebilidades.Text = txtDebilidades.Text;
            lblOportunidades.Text = txtOportunidades.Text;
            lblAmenazas.Text = txtAmenazas.Text;

            if (Session["ContextoId"] != null && int.TryParse(Session["ContextoId"].ToString(), out int contextoId))
            {
                Debug.WriteLine("btnGuardarFODA_Click - Intentando guardar FODA. ContextoId: " + contextoId);

                using (var context = new MyDbContext())
                {
                    var contextoExistente = context.Contextos.FirstOrDefault(c => c.ContextoId == contextoId);
                    if (contextoExistente == null)
                    {
                        Debug.WriteLine("Error: El Contexto no existe. ContextoId: " + contextoId);
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error: El Contexto no existe.');", true);
                        return;
                    }

                    var fodaExistente = context.Fodas.FirstOrDefault(f => f.ContextoId == contextoId);
                    if (fodaExistente != null)
                    {
                        fodaExistente.Fortalezas = txtFortalezas.Text;
                        fodaExistente.Debilidades = txtDebilidades.Text;
                        fodaExistente.Oportunidades = txtOportunidades.Text;
                        fodaExistente.Amenazas = txtAmenazas.Text;
                        Debug.WriteLine("FODA existente actualizado.");
                    }
                    else
                    {
                        var foda = new Foda
                        {
                            ContextoId = contextoId,
                            Fortalezas = txtFortalezas.Text,
                            Debilidades = txtDebilidades.Text,
                            Oportunidades = txtOportunidades.Text,
                            Amenazas = txtAmenazas.Text,
                        };
                        context.Fodas.Add(foda);
                        Debug.WriteLine("Nuevo FODA agregado.");
                    }

                    context.SaveChanges();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('FODA guardado exitosamente.');", true);
            }
            else
            {
                Debug.WriteLine("Error: No se ha seleccionado un Contexto.");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error: No se ha seleccionado un Contexto.');", true);
            }

            txtFortalezas.Text = "";
            txtDebilidades.Text = "";
            txtOportunidades.Text = "";
            txtAmenazas.Text = "";

            CargarFODA.Visible = false;
            MostrarFODA.Visible = true;
        }

        protected void btnMapeoProcesos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/MapeoProcesos");
            Debug.WriteLine("Redirigiendo a MapeoProcesos.aspx");
        }

        protected void btnFactoresExternos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Factores.aspx");
            Debug.WriteLine("Redirigiendo a Factores.aspx");
        }

        protected void btnAlcanceSistema_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/ObjetivoAlcanceDetallado.aspx");
            Debug.WriteLine("Redirigiendo a ObjetivoAlcanceDetallado.aspx");
        }
    }
}
