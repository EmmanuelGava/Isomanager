using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Isomanager.Models;    

namespace Isomanager
{
    public class UsuarioHelper
    {
        // Método para obtener todos los usuarios
        public static List<Usuarios> ObtenerTodosLosUsuarios()
        {
            using (var context = new MyDbContext())
            {
                return context.Usuarios.ToList();
            }
        }

        // Método para obtener un usuario por ID
        public static Usuarios ObtenerUsuarioPorId(int id)
        {
            using (var context = new MyDbContext())
            {
                return context.Usuarios.Find(id);
            }
        }



        // Método para crear un nuevo usuario y devolverlo
        public static Usuarios CrearUsuario(string nombre, string email, string rol)
        {
            // Verificar si el usuario ya existe
            if (ExisteUsuarioPorEmail(email))
            {
                throw new InvalidOperationException("El usuario ya existe con este correo electrónico.");
            }

            var nuevoUsuario = new Usuarios
            {
                Nombre = nombre,
                Email = email,
                Rol = rol
            };

            using (var context = new MyDbContext())
            {
                context.Usuarios.Add(nuevoUsuario);
                context.SaveChanges(); // Guarda el nuevo usuario
            }

            return nuevoUsuario; // Retorna el nuevo usuario
        }

        // Método para actualizar un usuario existente
        public static void ActualizarUsuario(Usuarios usuarioActualizado)
        {
            using (var context = new MyDbContext())
            {
                var usuario = context.Usuarios.Find(usuarioActualizado.UsuarioId);
                if (usuario != null)
                {
                    usuario.Nombre = usuarioActualizado.Nombre;
                    usuario.Email = usuarioActualizado.Email;
                    usuario.Rol = usuarioActualizado.Rol;
                    // Otros campos según sea necesario

                    context.SaveChanges();
                }
            }
        }

        // Método para eliminar un usuario por ID
        public static void EliminarUsuario(int id)
        {
            using (var context = new MyDbContext())
            {
                var usuario = context.Usuarios.Find(id);
                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    context.SaveChanges();
                }
            }
        }

        // Método para verificar si un usuario ya existe por email
        public static bool ExisteUsuarioPorEmail(string email)
        {
            using (var context = new MyDbContext())
            {
                return context.Usuarios.Any(u => u.Email == email);
            }
        }
    }

}