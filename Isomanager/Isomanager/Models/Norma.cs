using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Isomanager.Models
{
    public class Norma
    {
        [Key]
        public int NormaId { get; set; }  // Clave primaria
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public string Estado { get; set; }

        // Clave foránea para el responsable
        public int ResponsableId { get; set; } // Clave foránea

        // Relación con la entidad Usuarios
        public virtual Usuarios Responsable { get; set; } // Navegación a la entidad Usuarios

        public DateTime FechaCreacion { get; set; }

        // Relación uno a muchos con Contexto
        public virtual ICollection<Contexto> Contextos { get; set; }  // Colección de Contextos
    }
}