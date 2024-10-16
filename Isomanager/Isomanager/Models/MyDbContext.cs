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
        public DbSet<Mejora> Mejora { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

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

            modelBuilder.Entity<Norma>()
        .HasOptional(n => n.Contexto)  // Norma tiene un Contexto opcional
        .WithRequired(c => c.Norma)    // Contexto tiene una Norma requerida
        .Map(m => m.MapKey("NormaId"));  // Define la clave foránea

            // Relación uno a muchos entre Contexto y Proceso
            modelBuilder.Entity<Contexto>()
                .HasMany(c => c.Procesos) // Un Contexto tiene muchos Procesos
                .WithRequired(p => p.Contexto) // Cada Proceso requiere un Contexto
                .HasForeignKey(p => p.ContextoId) // Especificar la clave foránea
                .WillCascadeOnDelete(false); // No eliminar Procesos al eliminar Contexto

            // Relación uno a muchos entre Contexto y FactoresExternos
            modelBuilder.Entity<Contexto>()
                .HasMany(c => c.FactoresExternos)
                .WithRequired(f => f.Contexto)
                .HasForeignKey(f => f.ContextoId)  // Clave foránea explícita
                .WillCascadeOnDelete(false);

            // Relación uno a uno entre Contexto y Foda (ajustada)
            modelBuilder.Entity<Contexto>()
                .HasOptional(c => c.Foda)  // Contexto puede tener un Foda opcional
                .WithRequired(f => f.Contexto)  // Foda debe tener un Contexto
                .WillCascadeOnDelete(false);  // Evitar borrado en cascada

            // Relación uno a uno entre Contexto y AlcanceSistemaGestion
            modelBuilder.Entity<Contexto>()
                .HasOptional(c => c.AlcanceSistemaGestion)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

    }
}
