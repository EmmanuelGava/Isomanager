﻿
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isomanager.Models
{
    public class MapeoProcesosInternos
    {
        [Key]
        public int MapeoId { get; set; }  // Primary key

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; }  // Descripción del mapeo

        [Required]
        public DateTime FechaCreacion { get; set; }  // Fecha de creación

        public int NormaId { get; set; }  // Clave foránea que se relaciona con Norma (y Contexto)

        [ForeignKey("NormaId")]
        public virtual Contexto Contexto { get; set; }  // Relación con el Contexto
    }
}
