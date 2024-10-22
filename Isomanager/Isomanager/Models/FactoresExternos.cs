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

        [Required]
        public string TipoFactor { get; set; } // Tipo de factor externo

        [Required]
        public string Impacto { get; set; } // Nivel de impacto

        [Required]
        public string Probabilidad { get; set; } // Probabilidad

        [Required]
        public string AccionesSugeridas { get; set; } // Acciones sugeridas

        public string Responsable { get; set; } // Responsable

        // Clave foránea que se relaciona con Contexto (si es necesario)
        public int ContextoId { get; set; }

        [ForeignKey("ContextoId")]
        public virtual Contexto Contexto { get; set; }
    }
}