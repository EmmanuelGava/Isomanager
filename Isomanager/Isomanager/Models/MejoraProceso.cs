using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    public class MejoraProceso
    {
        [Key]
        public int MejoraId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public int ProcesoId { get; set; }
        [ForeignKey("ProcesoId")]
        public Proceso Proceso { get; set; }

        public int? UsuarioId { get; set; }  // ID del usuario que sugirió la mejora
        public Usuarios SugeridoPor { get; set; }  // Propiedad de navegación hacia el usuario

        // Propiedades adicionales de la clase Mejora
        public string AreaMejora { get; set; }
        public string AccionRecomendada { get; set; }
        public string Responsable { get; set; }
        public DateTime? FechaImplementacion { get; set; } // Puede ser nulo si no se ha implementado
    }
}