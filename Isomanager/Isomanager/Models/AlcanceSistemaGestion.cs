using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    [Table("AlcanceSistemaGestion")]
    public class AlcanceSistemaGestion
    {
        [Key]
        public int AlcanceId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public int NormaId { get; set; }  // Clave foránea que se relaciona con Norma (y Contexto)

        [ForeignKey("NormaId")]
        public virtual Contexto Contexto { get; set; }  // Relación con el Contexto }
    }
}
