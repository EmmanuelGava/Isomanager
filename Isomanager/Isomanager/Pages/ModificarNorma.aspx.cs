using System;
using System.Linq;
using System.Web.UI;
using Isomanager.Models;

namespace Isomanager
{
    public partial class ModificarNorma : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string documentId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(documentId))
                {
                    LoadDocument(documentId);
                }
                else
                {
                    lblMessage.Text = "ID de documento no proporcionado.";
                    lblMessage.Visible = true;
                }
            }
        }

        private void LoadDocument(string documentId)
        {
            using (var context = new MyDbContext())
            {
                var document = context.Documents.FirstOrDefault(d => d.Id == documentId);
                if (document != null)
                {
                    DocumentId.Text = document.Id;
                    Version.Text = document.Version;
                    Status.SelectedValue = document.Status;
                    ResponsiblePerson.Text = document.ResponsiblePerson;
                }
                else
                {
                    lblMessage.Text = "Documento no encontrado.";
                    lblMessage.Visible = true;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string documentId = DocumentId.Text;
            string version = Version.Text;
            string status = Status.SelectedValue;
            string responsiblePerson = ResponsiblePerson.Text;

            using (var context = new MyDbContext())
            {
                var document = context.Documents.FirstOrDefault(d => d.Id == documentId);
                if (document != null)
                {
                    document.Version = version;
                    document.Status = status;
                    document.ResponsiblePerson = responsiblePerson;
                    document.LastModified = DateTime.Now;

                    context.SaveChanges();
                    Response.Redirect("Normas.aspx");
                }
                else
                {
                    lblMessage.Text = "Error al guardar los cambios. Documento no encontrado.";
                    lblMessage.Visible = true;
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string documentId = DocumentId.Text;
            using (var context = new MyDbContext())
            {
                var documento = context.Documents.FirstOrDefault(d => d.Id == documentId);
                if (documento != null)
                {
                    context.Documents.Remove(documento);
                    context.SaveChanges();
                    Response.Redirect("Normas.aspx");
                }
                else
                {
                    lblMessage.Text = "Error: No se encontró el documento para eliminar.";
                    lblMessage.Visible = true;
                }
            }
        }
    }
}
