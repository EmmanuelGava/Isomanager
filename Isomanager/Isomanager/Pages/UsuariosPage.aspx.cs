using Isomanager.Models;
using System;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Isomanager.Pages
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            gvUsuarios.DataSource = UsuarioHelper.ObtenerTodosLosUsuarios();
            gvUsuarios.DataBind();
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string rol = ddlRol.SelectedValue;

            UsuarioHelper.CrearUsuario(nombre, email, rol);
            LimpiarFormulario();
            CargarUsuarios();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Asegúrate de que estás obteniendo el ID del usuario
                int index = Convert.ToInt32(e.CommandArgument);
                int usuarioId = Convert.ToInt32(gvUsuarios.DataKeys[index].Value); // Asegúrate de que DataKeys contenga el ID

                UsuarioHelper.EliminarUsuario(usuarioId); // Llama al método con el ID
                CargarUsuarios();
            }
            else if (e.CommandName == "Editar")
            {
                // Obtener el índice del usuario
                int index = Convert.ToInt32(e.CommandArgument);

                // Obtener el ID del usuario desde DataKeys
                int usuarioId = Convert.ToInt32(gvUsuarios.DataKeys[index].Value); // Asegúrate de que DataKeys contenga el ID

                // Cargar los datos del usuario en el formulario
                CargarDatosUsuario(usuarioId); // Llama al método para cargar los datos del usuario
            }
        }

        private void CargarDatosUsuario(int usuarioId)
        {
            // Obtener el usuario por ID
            var usuario = UsuarioHelper.ObtenerUsuarioPorId(usuarioId);
            if (usuario != null)
            {
                // Cargar los datos en los controles del formulario
                txtNombre.Text = usuario.Nombre;
                txtEmail.Text = usuario.Email;
                ddlRol.SelectedValue = usuario.Rol;

                // Cambiar el texto del botón para indicar que se está editando
                btnAgregarUsuario.Text = "Actualizar Usuario"; // Cambiar el texto del botón
                ViewState["UsuarioId"] = usuarioId; // Guardar el ID del usuario en ViewState para usarlo al actualizar
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlRol.SelectedIndex = 0;
        }
    }
}
