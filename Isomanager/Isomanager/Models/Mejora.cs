using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Isomanager.Models
{
    public class Mejora
    {
        [Key]
        public string Proceso { get; set; }
        public string AreaMejora { get; set; }
        public string AccionRecomendada { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaImplementacion { get; set; }
    }
}