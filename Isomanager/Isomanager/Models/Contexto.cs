using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    [Table("Contexto")]
    public class Contexto
    {
        [Key]
        public int ContextoId { get; set; }  // Clave primaria de Contexto

        // Relación uno a muchos con Procesos
        public virtual ICollection<Proceso> Procesos { get; set; } = new HashSet<Proceso>();

        // Relación uno a muchos con FactoresExternos
        public virtual ICollection<FactoresExternos> FactoresExternos { get; set; } = new HashSet<FactoresExternos>();

        public virtual AlcanceSistemaGestion AlcanceSistemaGestion { get; set; }

        // Relación uno a uno con Foda (ajustada)
        public virtual Foda Foda { get; set; }  // Relación opcional

        // Relación con Norma (propiedad de navegación)
        public virtual Norma Norma { get; set; }  // Relación uno a uno
    }
}
