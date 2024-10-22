// path/to/your/project/Factores.aspx.cs
using System;
using Isomanager.Models;

namespace Isomanager.Pages
{
    public partial class Factores : System.Web.UI.Page
    {
        private MyDbContext db = new MyDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTiposFactores(); // Cargar tipos de factores desde la base de datos si es necesario
            }
        }

        private void LoadTiposFactores()
        {
            // Aquí puedes cargar tipos de factores desde la base de datos si es necesario
            // Por ejemplo:
            // var tipos = db.TiposFactores.ToList();
            // foreach (var tipo in tipos)
            // {
            //     tipoFactor.Items.Add(new ListItem(tipo.Nombre, tipo.Id.ToString()));
            // }
        }

        protected void factorForm_Submit(object sender, EventArgs e)
        {
            var newFactor = new FactoresExternos
            {
                Descripcion = descripcion.Value,
                FechaCreacion = DateTime.Parse(fechaCreacion.Value),
                TipoFactor = tipoFactor.SelectedValue,
                Impacto = impacto.SelectedValue,
                Probabilidad = probabilidad.SelectedValue,
                AccionesSugeridas = accionesSugeridas.Value,
                Responsable = responsable.Value
            };

            db.FactoresExternos.Add(newFactor);
            db.SaveChanges();
            // Aquí puedes redirigir o mostrar un mensaje de éxito
        }
    }
}