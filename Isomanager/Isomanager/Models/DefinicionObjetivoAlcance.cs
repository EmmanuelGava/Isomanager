using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    public class DefinicionObjetivoAlcance
    {
        [Key, ForeignKey("Contexto")]  // Clave primaria y foránea al mismo tiempo
        public int ContextoId { get; set; }  // Usamos ContextoId como clave primaria y foránea
        public virtual Contexto Contexto { get; set; }  // Relación con el Contexto
        
        
        public string Objetivo { get; set; }
        public string Alcance { get; set; }

        

        // Propiedades de navegación para áreas y ubicaciones
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<UbicacionGeografica> Ubicaciones { get; set; }
    }
}