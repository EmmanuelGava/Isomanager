using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    public partial class Norma
    {
        [Key]
        public int NormaId { get; set; }  // Primary key
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public string Estado { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relación uno a uno con Contexto
      
        public virtual Contexto Contexto { get; set; }  // Referencia al objeto Contexto
    }
}
