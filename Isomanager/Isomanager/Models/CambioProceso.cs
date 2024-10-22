using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class CambioProceso
    {
        [Key]
        public int CambioId { get; set; }
        public string TipoCambio { get; set; }
        public string Descripcion { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaCambio { get; set; }

        public int ProcesoId { get; set; }
        public Proceso Proceso { get; set; }

        public int? UsuarioId { get; set; }  // ID del usuario que realizó el cambio
        public Usuarios RealizadoPor { get; set; }  // Propiedad de navegación hacia el usuario
    }

}