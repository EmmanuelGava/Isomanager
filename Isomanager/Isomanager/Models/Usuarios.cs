using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Isomanager.Models
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }  // Identificador único del usuario
        public string Nombre { get; set; }  // Nombre completo del usuario
        public string Email { get; set; }  // Correo electrónico del usuario
        public string Rol { get; set; }  // Rol del usuario (Ej.: Responsable, Auditor, etc.)

        // Propiedad que representa los procesos relacionados con este usuario
        public virtual ICollection<Proceso> Procesos { get; set; }

        // Relación con las acciones que este usuario ha realizado
        public List<Proceso> ProcesosAsignados { get; set; }  // Procesos en los que este usuario es responsable
        public List<CambioProceso> CambiosRealizados { get; set; }  // Cambios que este usuario ha realizado
        public List<AuditoriaInternaProceso> AuditoriasRealizadas { get; set; }  // Auditorías que este usuario ha realizado
        public List<MejoraProceso> MejorasSugeridas { get; set; }  // Mejoras sugeridas por este usuario

        public Usuarios()
        {
            ProcesosAsignados = new List<Proceso>();
            CambiosRealizados = new List<CambioProceso>();
            AuditoriasRealizadas = new List<AuditoriaInternaProceso>();
            MejorasSugeridas = new List<MejoraProceso>();
        }
    }

}