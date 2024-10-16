using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    public partial class Foda
    {
        [Key, ForeignKey("Contexto")]  // Clave primaria y foránea al mismo tiempo
        public int ContextoId { get; set; }  // Usamos ContextoId como clave primaria y foránea
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
