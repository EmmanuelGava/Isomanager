using Isomanager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class UbicacionGeografica
    {

        public int Id { get; set; }
        public string Nombre { get; set; } // Nombre de la ubicación

        // Relación con DefinicionObjetivoAlcance
        public int DefinicionObjetivoAlcanceId { get; set; }
        public virtual DefinicionObjetivoAlcance DefinicionObjetivoAlcance { get; set; }
    }
}