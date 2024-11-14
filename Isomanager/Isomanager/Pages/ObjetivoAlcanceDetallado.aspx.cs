using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Isomanager.Models;


namespace Isomanager.Pages
{
    

    public partial class ObjetivoAlcanceDetallado : System.Web.UI.Page
    {
      

        private MyDbContext db = new MyDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var db = new MyDbContext())
                {
                    if (Session["ContextoId"] != null && int.TryParse(Session["ContextoId"].ToString(), out int contextoId))
                    {
                        var definicionExistente = db.DefinicionesObjetivoAlcance.FirstOrDefault(d => d.ContextoId == contextoId);

                        if (definicionExistente == null)
                        {
                            var nuevaDefinicion = new DefinicionObjetivoAlcance
                            {
                                ContextoId = contextoId
                            };

                            db.DefinicionesObjetivoAlcance.Add(nuevaDefinicion);
                            db.SaveChanges();

                            definicionExistente = nuevaDefinicion;
                        }

                        if (definicionExistente != null)
                        {
                            LoadAreas();
                        }
                    }
                    else
                    {
                        // Manejar el caso donde ContextoId es nulo o no es un entero válido
                        lblError.Text = "ContextoId no válido.";
                    }
                }
            }
        }


        protected void btnAgregarArea_Click(object sender, EventArgs e)
        {
            string nuevaArea = txtNuevaArea.Text.Trim();

            if (string.IsNullOrEmpty(nuevaArea))
            {
                lblError.Text = "Por favor, ingresa un nombre para la nueva área.";
                return;
            }
            using (var db = new MyDbContext()) // Cambia a tu DbContext
            {
                var Area = new Area // Asegúrate de que esta clase esté bien definida
                {
                    Nombre = nuevaArea,
                    Activo = true,
                    ContextoId = (int)Session["ContextoId"]

                };

                db.Areas.Add(Area);
                db.SaveChanges();
            }



            // Guardar la nueva área en la base de datos

            txtNuevaArea.Text = string.Empty; // Limpiar el campo de texto
            LoadAreas(); // Recargar áreas
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            
            int areaId = int.Parse(btnEliminar.CommandArgument);

            // Eliminar el área de la base de datos
            DeleteArea(areaId);

            LoadAreas(); // Recargar áreas después de eliminar
        }

        private void LoadAreas()
        {
            using (var db = new MyDbContext()) // Cambia a tu DbContext
            {
                var areas = db.Areas.ToList(); // Obtener todas las áreas
                lvAreas.DataSource = areas;
                lvAreas.DataBind();
            }
        }

     

        private void DeleteArea(int areaId)
        {
            using (var db = new MyDbContext()) // Cambia a tu DbContext
            {
                var area = db.Areas.Find(areaId);
                if (area != null)
                {
                    db.Areas.Remove(area);
                    db.SaveChanges();
                }
            }
        }

        protected void btnGuardarDefinicion_Click(object sender, EventArgs e)
        {
            string objetivo = txtPropositoPrincipal.Text.Trim();
            string alcance = txtResultadosEsperados.Text.Trim();

            // Validación básica
            if (string.IsNullOrEmpty(objetivo) || string.IsNullOrEmpty(alcance))
            {
                lblError.Text = "Por favor, completa ambos campos.";
                return;
            }

            // Guardar en la base de datos
            SaveDefinition(objetivo, alcance);

            // Mostrar un mensaje de éxito
            lblSuccess.Text = "Definición guardada exitosamente.";
            txtPropositoPrincipal.Text = string.Empty; // Limpiar el campo de texto
            txtResultadosEsperados.Text = string.Empty;   // Limpiar el campo de texto
        }

        private void SaveDefinition(string objetivo, string alcance)
        {
            using (var db = new MyDbContext()) 
            {
                var definicion = new DefinicionObjetivoAlcance // Asegúrate de que esta clase esté bien definida
                {
                    Objetivo = objetivo,
                    Alcance = alcance
                };

                //db.DefinicionObjetivoAlcance.Add(definicion);
                db.SaveChanges();
            }
        }

    
    }
}
