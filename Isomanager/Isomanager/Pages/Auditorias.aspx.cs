using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Isomanager.Pages
{
    public partial class Auditorias : System.Web.UI.Page
    {
     
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    CargarAuditorias();
                }
            }

            protected void RegistrarAuditoria_Click(object sender, EventArgs e)
            {
                // Aquí puedes agregar la lógica para guardar la auditoría en la base de datos.
                // Ejemplo: GuardarAuditoria(NormaAuditada.Text, FechaAuditoria.Text, AuditorResponsable.Text, EstadoAuditoria.SelectedValue);

                // Luego, recargar la lista de auditorías
                CargarAuditorias();
            }

            private void CargarAuditorias()
            {
                // Aquí puedes agregar la lógica para obtener las auditorías de la base de datos y llenar el GridView.
                // Ejemplo: gvAuditorias.DataSource = ObtenerAuditorias();
                // gvAuditorias.DataBind();
            }
        }
}