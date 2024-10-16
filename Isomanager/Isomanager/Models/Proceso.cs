using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    [Table("Proceso")]
    public class Proceso
    {
        [Key]
        public int ProcesoId { get; set; }
        public string Nombre { get; set; }
        public string Propietario { get; set; }
        public string Objetivo { get; set; }

        // Clave foránea que se relaciona con Contexto
        [ForeignKey("Contexto")]
        public int ContextoId { get; set; }  // Relación con Contexto

        public virtual Contexto Contexto { get; set; }  // Propiedad de navegación
    }
}
