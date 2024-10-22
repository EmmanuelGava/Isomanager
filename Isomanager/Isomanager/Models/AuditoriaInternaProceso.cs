using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class AuditoriaInternaProceso
    {
        [Key]
        public int AuditoriaId { get; set; }
        public DateTime FechaAuditoria { get; set; }
        public string Responsable { get; set; }
        public string Comentarios { get; set; }

        public int ProcesoId { get; set; }
        public Proceso Proceso { get; set; }

        public int? UsuarioId { get; set; }  // ID del auditor
        public Usuarios Auditor { get; set; }  // Propiedad de navegación hacia el usuario auditor

    }

}