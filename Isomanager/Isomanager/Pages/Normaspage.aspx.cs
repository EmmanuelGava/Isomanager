using System;
using System.Web.UI;

namespace Isomanager
{
    public partial class Normaspage : Page  // Asegúrate de que hereda de Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDocuments();
            }
        }

        private void LoadDocuments()
        {
            // Tu código para cargar los documentos
        }
    }
}
