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

        //Relacion tipoFactor 
        public int TipoFactorId { get; set; }
        public virtual TipoFactor TipoFactor { get; set; }

        [Required]
        public string Impacto { get; set; } // Nivel de impacto

        [Required]
        public string Probabilidad { get; set; } // Probabilidad

        [Required]
        public string AccionesSugeridas { get; set; } // Acciones sugeridas

        public string Responsable { get; set; } // Responsable

        // Clave foránea que se relaciona con Contexto
        [ForeignKey("Contexto")]
        public int ContextoId { get; set; }  // Relación con Contexto
        public virtual Contexto Contexto { get; set; }  // Propiedad de navegación


    }
}