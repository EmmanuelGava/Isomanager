using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    public class FactoresExternos
    {
        [Key]
        public int FactoresExternosId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        // Clave foránea que se relaciona con Contexto
        public int ContextoId { get; set; }

        [ForeignKey("ContextoId")]
        public virtual Contexto Contexto { get; set; }
    }

}
