using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.Data.SqlClient;

namespace Isomanager.Models
{
    public class MyDbContext : DbContext
    {
        // DbSets para las entidades
        public DbSet<Document> Documents { get; set; }
        public DbSet<Norma> Normas { get; set; }
        public DbSet<Contexto> Contextos { get; set; }
        public DbSet<DefinicionObjetivoAlcance> DefinicionesObjetivoAlcance { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<UbicacionGeografica> UbicacionesGeograficas { get; set; }
        public DbSet<Foda> Fodas { get; set; }
        public DbSet<Proceso> Procesos { get; set; }

        public DbSet<FactoresExternos> FactoresExternos { get; set; }
        public DbSet<TipoFactor> TiposFactores { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<CambioProceso> CambioProcesos { get; set; }  // Agrega el DbSet para CambioProceso
        public DbSet<AuditoriaInternaProceso> AuditoriaInternaProcesos { get; set; }  // Agrega el DbSet para AuditoriaInternaProceso
        public DbSet<MejoraProceso> MejoraProcesos { get; set; }  // Agrega el DbSet para MejoraProceso
        public DbSet<KPI> KPI { get; set; }
        public DbSet<EvaluacionProceso> EvaluacionProcesos { get; set; }


        // Constructor para la conexión a la base de datos
        public MyDbContext() : base("name=isomanagerDB")
        {
            // Database.SetInitializer(new CreateDatabaseIfNotExists<MyDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quitar la convención de pluralización automática de nombres de tablas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            // Configurar la relación uno a muchos entre Norma y Contexto
            modelBuilder.Entity<Norma>()
                .HasMany(n => n.Contextos) // Una Norma puede tener muchos Contextos
                .WithRequired(c => c.Norma) // Cada Contexto requiere una Norma
                .HasForeignKey(c => c.NormaId); // La propiedad de clave foránea en Contexto

           modelBuilder.Entity<Norma>()
                .HasRequired(n => n.Responsable) // Norma requiere un responsable
                .WithMany(u => u.Normas) // Un usuario puede tener muchas normas
                .HasForeignKey(n => n.ResponsableId); // Clave foránea


            // Relación uno a uno entre Contexto y DefinicionObjetivoAlcance
            modelBuilder.Entity<Contexto>()
                .HasOptional(c => c.DefinicionObjetivoAlcance)
                .WithRequired(d => d.Contexto);

            // Configurar la relación uno a muchos entre DefinicionObjetivoAlcance y Area
            modelBuilder.Entity<DefinicionObjetivoAlcance>()
                .HasMany(d => d.Areas)
                .WithRequired(a => a.DefinicionObjetivoAlcance)
                .HasForeignKey(a => a.ContextoId);

            // Configurar la relación uno a muchos entre DefinicionObjetivoAlcance y UbicacionGeografica
            modelBuilder.Entity<DefinicionObjetivoAlcance>()
                .HasMany(d => d.Ubicaciones)
                .WithRequired(u => u.DefinicionObjetivoAlcance)
                .HasForeignKey(u => u.DefinicionObjetivoAlcanceId);

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


            modelBuilder.Entity<TipoFactor>()
                .HasMany(t => t.FactoresExternos)
                .WithRequired(f => f.TipoFactor)
                .HasForeignKey(f => f.TipoFactorId)
                .WillCascadeOnDelete(false);


        }


    }
}
