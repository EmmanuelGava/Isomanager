using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Isomanager.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace Isomanager
{
    public class MyDbContext : DbContext
    {
        // DbSets para las entidades
        public DbSet<Document> Documents { get; set; }
        public DbSet<Norma> Normas { get; set; }
        public DbSet<Contexto> Contextos { get; set; }
        public DbSet<Foda> Fodas { get; set; }
        public DbSet<MapeoProcesosInternos> MapeoProcesosInternos { get; set; }
        public DbSet<AlcanceSistemaGestion> AlcanceSistemaGestiones { get; set; }
        public DbSet<FactoresExternos> FactoresExternos { get; set; }
        public DbSet<Mejora> Mejora { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        // Constructor para la conexión a la base de datos
        public MyDbContext() : base("name=MyDbContext")
        {
        }

        // Configuración de las relaciones en el modelo
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quitar la convención de pluralización automática de nombres de tablas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Relación entre Norma y Contexto
            modelBuilder.Entity<Norma>()
        .HasOptional(n => n.Contexto)
        .WithRequired(c => c.Norma);

            // Relación uno a uno entre Contexto y AlcanceSistemaGestion
            modelBuilder.Entity<Contexto>()
                .HasOptional(c => c.AlcanceSistemaGestion)
                .WithMany()  // 'Alcance' puede estar asociado con muchos 'Contexto'
                .HasForeignKey(c => c.AlcanceId)
                .WillCascadeOnDelete(false);  // Evitar que se borre 'Alcance' si se elimina 'Contexto'

            // Relación uno a uno entre Contexto y FactoresExternos
            modelBuilder.Entity<Contexto>()
                .HasOptional(c => c.FactoresExternos)
                .WithMany()  // 'FactoresExternos' puede estar asociado con muchos 'Contexto'
                .HasForeignKey(c => c.FactoresExternosId)
                .WillCascadeOnDelete(false);  // Evitar borrado en cascada

            // Relación uno a uno entre Contexto y MapeoProcesosInternos
            modelBuilder.Entity<Contexto>()
                .HasOptional(c => c.MapeoProcesosInternos)
                .WithMany()  // 'MapeoProcesosInternos' puede estar asociado con muchos 'Contexto'
                .HasForeignKey(c => c.MapeoId)
                .WillCascadeOnDelete(false);  // Evitar borrado en cascada
        }
    }
}
