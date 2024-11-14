using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class TipoFactor
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public virtual ICollection<FactoresExternos> FactoresExternos { get; set; }
    }
}