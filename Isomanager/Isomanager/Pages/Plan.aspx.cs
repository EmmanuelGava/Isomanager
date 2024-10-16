using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Isomanager.Models;

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

        // Cargar las normas en el DropDownList
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

        // Evento para manejar el cambio de selección en ddlNormas
        protected void ddlNormas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlNormas.SelectedValue, out int normaId) && normaId > 0)
            {
                Session["normaId"] = normaId; // Almacena el NormaId en la sesión
            }
            else
            {
                Session["normaId"] = null; // Limpia el valor en caso de selección inválida
            }
        }

        // Evento para guardar el contexto
        protected void btnGuardarContexto_Click(object sender, EventArgs e)
        {
            // Asegurarse de que el NormaId esté en la sesión y sea válido
            if (Session["normaId"] != null && int.TryParse(Session["normaId"].ToString(), out int normaId) && normaId > 0)
            {
                using (var context = new MyDbContext())
                {
                    try
                    {
                        var normaExistente = context.Normas.Include("Contexto")
                            .FirstOrDefault(n => n.NormaId == normaId);

                        if (normaExistente == null)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('La norma no existe.');", true);
                            return;
                        }

                        // Verificar si la norma ya tiene un contexto
                        if (normaExistente.Contexto == null)
                        {
                            // Crear un nuevo contexto solo si no tiene uno asociado
                            var nuevoContexto = new Contexto
                            {
                                ContextoId = normaId,  // Asignar el NormaId al nuevo contexto
                                Norma = normaExistente  // Establecer la relación con la norma
                            };

                            // Guardar el nuevo contexto en la base de datos
                            context.Contextos.Add(nuevoContexto);
                            context.SaveChanges();
                        }

                        // Almacenar el NormaId y el ContextoId en la sesión
                        Session["normaId"] = normaId;
                        Session["contextoId"] = normaId; // Asumiendo que el ContextoId es el mismo que el NormaId

                        // Redirigir a la página de Contextos
                        Response.Redirect("Contextos.aspx");
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('Error al guardar el contexto: {ex.Message}');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Por favor, seleccione una norma antes de guardar el contexto.');", true);
            }
        }
    }
}
