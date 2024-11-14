using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Isomanager.Models
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }  // Identificador único del usuario

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }  // Nombre completo del usuario

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }  // Correo electrónico del usuario

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Rol { get; set; }  // Rol del usuario (Ej.: Responsable, Auditor, etc.)

        // Propiedad que representa los procesos relacionados con este usuario
        public virtual ICollection<Proceso> Procesos { get; set; }

        // Relación con las acciones que este usuario ha realizado
        public List<Proceso> ProcesosAsignados { get; set; }  // Procesos en los que este usuario es responsable
        public List<CambioProceso> CambiosRealizados { get; set; }  // Cambios que este usuario ha realizado
        public List<AuditoriaInternaProceso> AuditoriasRealizadas { get; set; }  // Auditorías que este usuario ha realizado
        public List<MejoraProceso> MejorasSugeridas { get; set; }  // Mejoras sugeridas por este usuario
        public virtual ICollection<Norma> Normas { get; set; } // Colección de Normas
        public Usuarios()
        {
            ProcesosAsignados = new List<Proceso>();
            CambiosRealizados = new List<CambioProceso>();
            AuditoriasRealizadas = new List<AuditoriaInternaProceso>();
            MejorasSugeridas = new List<MejoraProceso>();
        }
    }
}