using System;
using System.Web.UI;
using Isomanager;
using System.Data.Entity;
using Isomanager.Models;    


namespace Isomanager.Pages
{
    public partial class CrearNorma : Page
    {
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos del formulario
            string titulo = Titulo.Value;
            string descripcion = Descripcion.Value;
            string version = Version.Value;
            string estado = Status.SelectedValue;
            string responsable = ResponsiblePerson.Value;

            // Verificar que los valores no estén vacíos
            if (!string.IsNullOrEmpty(titulo) &&
                !string.IsNullOrEmpty(descripcion) &&
                !string.IsNullOrEmpty(version) &&
                !string.IsNullOrEmpty(estado) &&
                !string.IsNullOrEmpty(responsable))
            {
                using (var context = new MyDbContext())
                {
                    // Crear una nueva instancia de la entidad Norma
                    var nuevaNorma = new Norma
                    {
                        Titulo = titulo,
                        Descripcion = descripcion,
                        Version = version,
                        Estado = estado,
                        Responsable = responsable,
                        FechaCreacion = DateTime.Now
                    };

                    // Crear una nueva instancia de Contexto y asociarla con la nueva Norma
                    var nuevoContexto = new Contexto
                    {
                        Norma = nuevaNorma
                        // Aquí puedes agregar otras propiedades del Contexto si las hay
                    };

                    // Agregar tanto la Norma como el Contexto al contexto de EF
                    context.Normas.Add(nuevaNorma);
                    context.Contextos.Add(nuevoContexto);

                    // Guardar todos los cambios de una vez
                    context.SaveChanges();
                }

                // Redireccionar a la página de Normas después de guardar
                Response.Redirect("Plan.aspx");
            }
            else
            {
                // Manejar el caso cuando los campos estén vacíos
                // Puedes mostrar un mensaje de error aquí
            }
        }
    }
}