using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class KPI
    {
        [Key]
        public int KpiId { get; set; }  // Identificador único del KPI
        public string Nombre { get; set; }  // Nombre del indicador
        public string Valor { get; set; }  // Valor del indicador medido
        public DateTime FechaMedicion { get; set; }  // Fecha en que se midió el indicador
        public int ProcesoId { get; set; }  // Referencia al proceso
        public Proceso Proceso { get; set; }  // Propiedad de navegación hacia el proceso
    }

}