
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    [Table("Contexto")]
    public partial class Contexto
    {
        public Contexto()
        {
            Fodas = new HashSet<Foda>();
        }

        [Key]
        [ForeignKey("Norma")]
        public int NormaId { get; set; }  // Esta es ahora tanto la clave primaria como la clave foránea

        // Relación con Norma
        public virtual Norma Norma { get; set; }

        // Relaciones con otras entidades
        public virtual ICollection<Foda> Fodas { get; set; }

        public int? AlcanceId { get; set; }
        [ForeignKey("AlcanceId")]
        public virtual AlcanceSistemaGestion AlcanceSistemaGestion { get; set; }

        public int? FactoresExternosId { get; set; }
        [ForeignKey("FactoresExternosId")]
        public virtual FactoresExternos FactoresExternos { get; set; }

        public int? MapeoId { get; set; }
        [ForeignKey("MapeoId")]
        public virtual MapeoProcesosInternos MapeoProcesosInternos { get; set; }

        // Puedes agregar aquí otras propiedades específicas de Contexto si las necesitas
    }
}