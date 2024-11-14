using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Isomanager.Models;
using System.Data.Entity;

namespace Isomanager.Pages
{
    public partial class Plan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarNormas(); // Llama al método para cargar normas al inicio

                // Verificar si hay un NormaId en la sesión y obtener el ContextoId
                if (Session["NormaId"] != null && int.TryParse(Session["NormaId"].ToString(), out int normaId) && normaId > 0)
                {
                    ObtenerContextoId(normaId); // Obtener el ContextoId solo si es la primera carga
                }
            }
        }

        // Cargar las normas en el DropDownList
        private void CargarNormas()
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    var normas = context.Normas.Select(n => new { n.NormaId, n.Titulo }).ToList();
                    ddlNormas.DataSource = normas;
                    ddlNormas.DataTextField = "Titulo";
                    ddlNormas.DataValueField = "NormaId";
                    ddlNormas.DataBind();
                    ddlNormas.Items.Insert(0, new ListItem("Seleccione una norma", "0"));
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error al cargar normas: {ex.Message}');", true);
                }
            }
        }

        // Obtener el ContextoId asociado a la norma
        private void ObtenerContextoId(int normaId)
        {
            using (var context = new MyDbContext())
            {
                var norma = context.Normas.Include(n => n.Contextos)
                    .FirstOrDefault(n => n.NormaId == normaId);

                if (norma != null)
                {
                    var contextoLocal = norma.Contextos.FirstOrDefault();
                    if (contextoLocal != null)
                    {
                        Session["ContextoId"] = contextoLocal.ContextoId; // Guardar el ContextoId
                    }
                    else
                    {
                        Session["ContextoId"] = null; // No hay contexto
                    }
                }
                else
                {
                    Session["ContextoId"] = null; // La norma no existe
                }
            }
        }

        // Evento para manejar el cambio de selección en ddlNormas
        protected void ddlNormas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlNormas.SelectedValue, out int normaId) && normaId > 0)
            {
                Session["NormaId"] = normaId; // Almacenar NormaId
                ObtenerContextoId(normaId); // Obtener el ContextoId
            }
            else
            {
                Session["NormaId"] = null; // Limpia el NormaId
                Session["ContextoId"] = null; // Limpia el ContextoId
            }
        }

        protected void btnGuardarContexto_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de Contextos
            Response.Redirect("Contextos.aspx");
        }
    }
}
