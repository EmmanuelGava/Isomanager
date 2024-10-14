using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class Mejora
    {
        public string Proceso { get; set; }
        public string AreaMejora { get; set; }
        public string AccionRecomendada { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaImplementacion { get; set; }
    }
}