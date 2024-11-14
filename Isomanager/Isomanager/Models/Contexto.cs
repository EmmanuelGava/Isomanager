using Isomanager.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Contexto
{
    [Key]
    public int ContextoId { get; set; }  // Clave primaria de Contexto

    // Clave foránea para Norma
    public int NormaId { get; set; }  // Asegúrate de que esta propiedad exista

    // Relación uno a muchos con Norma
    [ForeignKey("NormaId")]
    public virtual Norma Norma { get; set; }  // Relación uno a muchos

    // Otras relaciones...
    public virtual DefinicionObjetivoAlcance DefinicionObjetivoAlcance { get; set; }
    public virtual ICollection<Proceso> Procesos { get; set; }
    public virtual ICollection<FactoresExternos> FactoresExternos { get; set; }
    public virtual Foda Foda { get; set; }  // Relación opcional
}
