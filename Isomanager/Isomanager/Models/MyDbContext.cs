using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Isomanager.Models
{
    public class MyDbContext : DbContext
    {
        // DbSets para las entidades
        public DbSet<Document> Documents { get; set; }
        public DbSet<Norma> Normas { get; set; }
        public DbSet<Contexto> Contextos { get; set; }
        public DbSet<Foda> Fodas { get; set; }
        public DbSet<Proceso> Procesos { get; set; }
        public DbSet<AlcanceSistemaGestion> AlcanceSistemaGestiones { get; set; }
        public DbSet<FactoresExternos> FactoresExternos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        // Nuevas clases
        public DbSet<CambioProceso> CambioProcesos { get; set; }  // Agrega el DbSet para CambioProceso
        public DbSet<AuditoriaInternaProceso> AuditoriaInternaProcesos { get; set; }  // Agrega el DbSet para AuditoriaInternaProceso
        public DbSet<MejoraProceso> MejoraProcesos { get; set; }  // Agrega el DbSet para MejoraProceso
        public DbSet<KPI> KPI { get; set; }
        public DbSet<EvaluacionProceso>EvaluacionProcesos { get; set; }
        

        // Constructor para la conexión a la base de datos
        public MyDbContext() : base("name=MyDbContext")
        {
            // Database.SetInitializer(new CreateDatabaseIfNotExists<MyDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quitar la convención de pluralización automática de nombres de tablas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Relación entre Norma y Contexto (uno a uno opcional)
            modelBuilder.Entity<Norma>()
                .HasOptional(n => n.Contexto)
                .WithRequired(c => c.Norma)
                .Map(m => m.MapKey("NormaId"));

            // Relación entre Contexto y Proceso (uno a muchos)
            modelBuilder.Entity<Contexto>()
                .HasMany(c => c.Procesos)
                .WithRequired(p => p.Contexto)
                .HasForeignKey(p => p.ContextoId)
                .WillCascadeOnDelete(false);

            // Relación entre Contexto y FactoresExternos (uno a muchos)
            modelBuilder.Entity<Contexto>()
                .HasMany(c => c.FactoresExternos)
                .WithRequired(f => f.Contexto)
                .HasForeignKey(f => f.ContextoId)
                .WillCascadeOnDelete(false);

            // Relación entre Contexto y Foda (uno a uno opcional)
            modelBuilder.Entity<Contexto>()
                .HasOptional(c => c.Foda)
                .WithRequired(f => f.Contexto)
                .WillCascadeOnDelete(false);

            // Relación entre Contexto y AlcanceSistemaGestion (uno a uno opcional)
            modelBuilder.Entity<Contexto>()
                .HasOptional(c => c.AlcanceSistemaGestion)
                .WithMany()
                .WillCascadeOnDelete(false);

            // Relación entre Proceso y Usuario (responsable, obligatoria)
            modelBuilder.Entity<Proceso>()
                .HasRequired(p => p.Responsable)
                .WithMany(u => u.Procesos)
                .HasForeignKey(p => p.UsuarioId)
                .WillCascadeOnDelete(false);

            // Relación entre Proceso y MejoraProceso (uno a muchos)
            modelBuilder.Entity<MejoraProceso>()
                .HasRequired(m => m.Proceso)
                .WithMany(p => p.Mejoras)
                .HasForeignKey(m => m.ProcesoId)
                .WillCascadeOnDelete(true);

            // Relación entre Proceso y CambioProceso (uno a muchos)
            modelBuilder.Entity<CambioProceso>()
                .HasRequired(c => c.Proceso)
                .WithMany(p => p.Cambios)
                .HasForeignKey(c => c.ProcesoId)
                .WillCascadeOnDelete(true);

         

            // Relación entre Usuario y Proceso
            modelBuilder.Entity<Usuarios>()
                .HasMany(u => u.Procesos)  // Un Usuario puede tener muchos Procesos
                .WithRequired(p => p.Responsable)  // Cada Proceso tiene un Responsable (Usuario)
                .HasForeignKey(p => p.UsuarioId)
                .WillCascadeOnDelete(false);  // No eliminar el usuario si se elimina un proceso

            // Relación entre Usuario y CambioProceso
            modelBuilder.Entity<Usuarios>()
                .HasMany(u => u.CambiosRealizados)
                .WithRequired(c => c.RealizadoPor)
                .HasForeignKey(c => c.UsuarioId)
                .WillCascadeOnDelete(false);

            // Relación entre Usuario y AuditoriaInternaProceso
            modelBuilder.Entity<Usuarios>()
                .HasMany(u => u.AuditoriasRealizadas)
                .WithRequired(a => a.Auditor)
                .HasForeignKey(a => a.UsuarioId)
                .WillCascadeOnDelete(false);

            // Relación entre Usuario y MejoraProceso
            modelBuilder.Entity<Usuarios>()
                .HasMany(u => u.MejorasSugeridas)
                .WithRequired(m => m.SugeridoPor)
                .HasForeignKey(m => m.UsuarioId)
                .WillCascadeOnDelete(false);
        }


    }
}
