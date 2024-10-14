using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    public partial class Foda
    {
        [Key]
        public int FodaId { get; set; }  // Primary key

        public int NormaId { get; set; }  // Clave foránea que se relaciona con Norma (y Contexto)

        [ForeignKey("NormaId")]
        public virtual Contexto Contexto { get; set; }  // Relación con el Contexto
        // Propiedades para FODA
        [MaxLength]
        public string Fortalezas { get; set; }
        [MaxLength]
        public string Oportunidades { get; set; }
        [MaxLength]
        public string Debilidades { get; set; }
        [MaxLength]
        public string Amenazas { get; set; }
    }
}
