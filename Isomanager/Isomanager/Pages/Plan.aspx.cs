using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
namespace Isomanager.Pages
{
    public partial class Plan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarNormas();
            }
        }

        private void CargarNormas()
        {
            using (var context = new MyDbContext()) // Asegúrate de que MyDbContext esté definido
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

        protected void ddlNormas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlNormas.SelectedValue, out int normaId))
            {
                Session["normaId"] = normaId;
            }
        }
    }
}