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
                        AlcanceId = c.Int(),
                        NormaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContextoId)
                .ForeignKey("dbo.AlcanceSistemaGestion", t => t.AlcanceId)
                .ForeignKey("dbo.Norma", t => t.NormaId)
                .Index(t => t.AlcanceId)
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
                        Contexto_ContextoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProcesoId)
                .ForeignKey("dbo.Contexto", t => t.ContextoId, cascadeDelete: true)
                .ForeignKey("dbo.Contexto", t => t.Contexto_ContextoId)
                .Index(t => t.ContextoId)
                .Index(t => t.Contexto_ContextoId);
            
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
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Correo = c.String(),
                        Rol = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlcanceSistemaGestion", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Proceso", "Contexto_ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Proceso", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Contexto", "NormaId", "dbo.Norma");
            DropForeignKey("dbo.Foda", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.FactoresExternos", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Contexto", "AlcanceId", "dbo.AlcanceSistemaGestion");
            DropIndex("dbo.Proceso", new[] { "Contexto_ContextoId" });
            DropIndex("dbo.Proceso", new[] { "ContextoId" });
            DropIndex("dbo.Foda", new[] { "ContextoId" });
            DropIndex("dbo.FactoresExternos", new[] { "ContextoId" });
            DropIndex("dbo.Contexto", new[] { "NormaId" });
            DropIndex("dbo.Contexto", new[] { "AlcanceId" });
            DropIndex("dbo.AlcanceSistemaGestion", new[] { "ContextoId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Mejora");
            DropTable("dbo.Document");
            DropTable("dbo.Proceso");
            DropTable("dbo.Norma");
            DropTable("dbo.Foda");
            DropTable("dbo.FactoresExternos");
            DropTable("dbo.Contexto");
            DropTable("dbo.AlcanceSistemaGestion");
        }
    }
}
