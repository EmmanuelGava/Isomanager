using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class EvaluacionProceso
    {
        [Key]
        public int EvaluacionId { get; set; }  // Identificador único de la evaluación
        public DateTime Fecha { get; set; }  // Fecha en que se realizó la evaluación
        public string Observaciones { get; set; }  // Observaciones o resultados de la evaluación
        public string IndicadoresClaves { get; set; }  // Indicadores clave de desempeño (KPIs)
        public int ProcesoId { get; set; }  // Referencia al proceso evaluado
        public Proceso Proceso { get; set; }  // Propiedad de navegación hacia el proceso
    }

}