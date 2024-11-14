using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class Area
    {

        public int AreaId { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        // Clave foránea hacia ContextoId en DefinicionObjetivoAlcance
        public int ContextoId { get; set; }

        // Relación con DefinicionObjetivoAlcance usando ContextoId
        [ForeignKey("ContextoId")]
        public virtual DefinicionObjetivoAlcance DefinicionObjetivoAlcance { get; set; }
    }
}