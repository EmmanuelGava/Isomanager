namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlcanceSistemaGestion",
                c => new
                    {
                        AlcanceId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        FechaCreacion = c.DateTime(nullable: false),
                        ContextoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlcanceId)
                .ForeignKey("dbo.Contexto", t => t.ContextoId, cascadeDelete: true)
                .Index(t => t.ContextoId);
            
            CreateTable(
                "dbo.Contexto",
                c => new
                    {
                        ContextoId = c.Int(nullable: false, identity: true),
                        AlcanceSistemaGestion_AlcanceId = c.Int(),
                        NormaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContextoId)
                .ForeignKey("dbo.AlcanceSistemaGestion", t => t.AlcanceSistemaGestion_AlcanceId)
                .ForeignKey("dbo.Norma", t => t.NormaId)
                .Index(t => t.AlcanceSistemaGestion_AlcanceId)
                .Index(t => t.NormaId);
            
            CreateTable(
                "dbo.FactoresExternos",
                c => new
                    {
                        FactoresExternosId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        FechaCreacion = c.DateTime(nullable: false),
                        ContextoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FactoresExternosId)
                .ForeignKey("dbo.Contexto", t => t.ContextoId)
                .Index(t => t.ContextoId);
            
            CreateTable(
                "dbo.Foda",
                c => new
                    {
                        ContextoId = c.Int(nullable: false),
                        Fortalezas = c.String(),
                        Oportunidades = c.String(),
                        Debilidades = c.String(),
                        Amenazas = c.String(),
                    })
                .PrimaryKey(t => t.ContextoId)
                .ForeignKey("dbo.Contexto", t => t.ContextoId)
                .Index(t => t.ContextoId);
            
            CreateTable(
                "dbo.Norma",
                c => new
                    {
                        NormaId = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descripcion = c.String(),
                        Version = c.String(),
                        Estado = c.String(),
                        Responsable = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NormaId);
            
            CreateTable(
                "dbo.Proceso",
                c => new
                    {
                        ProcesoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Propietario = c.String(),
                        Objetivo = c.String(),
                        ContextoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        Usuarios_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.ProcesoId)
                .ForeignKey("dbo.Usuarios", t => t.Usuarios_UsuarioId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .ForeignKey("dbo.Contexto", t => t.ContextoId)
                .Index(t => t.ContextoId)
                .Index(t => t.UsuarioId)
                .Index(t => t.Usuarios_UsuarioId);
            
            CreateTable(
                "dbo.CambioProceso",
                c => new
                    {
                        CambioId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        ProcesoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CambioId)
                .ForeignKey("dbo.Proceso", t => t.ProcesoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.ProcesoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Email = c.String(),
                        Rol = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.AuditoriaInternaProceso",
                c => new
                    {
                        AuditoriaId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Comentarios = c.String(),
                        ProcesoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AuditoriaId)
                .ForeignKey("dbo.Proceso", t => t.ProcesoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.ProcesoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.MejoraProceso",
                c => new
                    {
                        MejoraId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        ProcesoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MejoraId)
                .ForeignKey("dbo.Proceso", t => t.ProcesoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.ProcesoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Version = c.String(),
                        Status = c.String(),
                        ResponsiblePerson = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mejora",
                c => new
                    {
                        Proceso = c.String(nullable: false, maxLength: 128),
                        AreaMejora = c.String(),
                        AccionRecomendada = c.String(),
                        Responsable = c.String(),
                        FechaImplementacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Proceso);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlcanceSistemaGestion", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Proceso", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Proceso", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Proceso", "Usuarios_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.MejoraProceso", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.MejoraProceso", "ProcesoId", "dbo.Proceso");
            DropForeignKey("dbo.CambioProceso", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.AuditoriaInternaProceso", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.AuditoriaInternaProceso", "ProcesoId", "dbo.Proceso");
            DropForeignKey("dbo.CambioProceso", "ProcesoId", "dbo.Proceso");
            DropForeignKey("dbo.Contexto", "NormaId", "dbo.Norma");
            DropForeignKey("dbo.Foda", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.FactoresExternos", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Contexto", "AlcanceSistemaGestion_AlcanceId", "dbo.AlcanceSistemaGestion");
            DropIndex("dbo.MejoraProceso", new[] { "UsuarioId" });
            DropIndex("dbo.MejoraProceso", new[] { "ProcesoId" });
            DropIndex("dbo.AuditoriaInternaProceso", new[] { "UsuarioId" });
            DropIndex("dbo.AuditoriaInternaProceso", new[] { "ProcesoId" });
            DropIndex("dbo.CambioProceso", new[] { "UsuarioId" });
            DropIndex("dbo.CambioProceso", new[] { "ProcesoId" });
            DropIndex("dbo.Proceso", new[] { "Usuarios_UsuarioId" });
            DropIndex("dbo.Proceso", new[] { "UsuarioId" });
            DropIndex("dbo.Proceso", new[] { "ContextoId" });
            DropIndex("dbo.Foda", new[] { "ContextoId" });
            DropIndex("dbo.FactoresExternos", new[] { "ContextoId" });
            DropIndex("dbo.Contexto", new[] { "NormaId" });
            DropIndex("dbo.Contexto", new[] { "AlcanceSistemaGestion_AlcanceId" });
            DropIndex("dbo.AlcanceSistemaGestion", new[] { "ContextoId" });
            DropTable("dbo.Mejora");
            DropTable("dbo.Document");
            DropTable("dbo.MejoraProceso");
            DropTable("dbo.AuditoriaInternaProceso");
            DropTable("dbo.Usuarios");
            DropTable("dbo.CambioProceso");
            DropTable("dbo.Proceso");
            DropTable("dbo.Norma");
            DropTable("dbo.Foda");
            DropTable("dbo.FactoresExternos");
            DropTable("dbo.Contexto");
            DropTable("dbo.AlcanceSistemaGestion");
        }
    }
}
