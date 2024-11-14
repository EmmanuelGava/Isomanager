using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class Formacion
    {
        [Key]
        public int Id { get; set; }
        public string Area { get; set; }
        public int Horas { get; set; }

        //Relacion con la entidad Usuarios
        public int UsuarioId { get; set; }
        public virtual Usuarios Usuario { get; set; }
    }
}