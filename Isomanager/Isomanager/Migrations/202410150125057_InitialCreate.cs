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
                        NormaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlcanceId)
                .ForeignKey("dbo.Contexto", t => t.NormaId, cascadeDelete: true)
                .Index(t => t.NormaId);
            
            CreateTable(
                "dbo.Contexto",
                c => new
                    {
                        NormaId = c.Int(nullable: false),
                        AlcanceId = c.Int(),
                        FactoresExternosId = c.Int(),
                        MapeoId = c.Int(),
                    })
                .PrimaryKey(t => t.NormaId)
                .ForeignKey("dbo.AlcanceSistemaGestion", t => t.AlcanceId)
                .ForeignKey("dbo.FactoresExternos", t => t.FactoresExternosId)
                .ForeignKey("dbo.MapeoProcesosInternos", t => t.MapeoId)
                .ForeignKey("dbo.Norma", t => t.NormaId)
                .Index(t => t.NormaId)
                .Index(t => t.AlcanceId)
                .Index(t => t.FactoresExternosId)
                .Index(t => t.MapeoId);
            
            CreateTable(
                "dbo.FactoresExternos",
                c => new
                    {
                        FactoresExternosId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        FechaCreacion = c.DateTime(nullable: false),
                        NormaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FactoresExternosId)
                .ForeignKey("dbo.Contexto", t => t.NormaId, cascadeDelete: true)
                .Index(t => t.NormaId);
            
            CreateTable(
                "dbo.Foda",
                c => new
                    {
                        FodaId = c.Int(nullable: false, identity: true),
                        NormaId = c.Int(nullable: false),
                        Fortalezas = c.String(),
                        Oportunidades = c.String(),
                        Debilidades = c.String(),
                        Amenazas = c.String(),
                    })
                .PrimaryKey(t => t.FodaId)
                .ForeignKey("dbo.Contexto", t => t.NormaId, cascadeDelete: true)
                .Index(t => t.NormaId);
            
            CreateTable(
                "dbo.MapeoProcesosInternos",
                c => new
                    {
                        MapeoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        FechaCreacion = c.DateTime(nullable: false),
                        NormaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MapeoId)
                .ForeignKey("dbo.Contexto", t => t.NormaId, cascadeDelete: true)
                .Index(t => t.NormaId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlcanceSistemaGestion", "NormaId", "dbo.Contexto");
            DropForeignKey("dbo.Contexto", "NormaId", "dbo.Norma");
            DropForeignKey("dbo.Contexto", "MapeoId", "dbo.MapeoProcesosInternos");
            DropForeignKey("dbo.MapeoProcesosInternos", "NormaId", "dbo.Contexto");
            DropForeignKey("dbo.Foda", "NormaId", "dbo.Contexto");
            DropForeignKey("dbo.Contexto", "FactoresExternosId", "dbo.FactoresExternos");
            DropForeignKey("dbo.FactoresExternos", "NormaId", "dbo.Contexto");
            DropForeignKey("dbo.Contexto", "AlcanceId", "dbo.AlcanceSistemaGestion");
            DropIndex("dbo.MapeoProcesosInternos", new[] { "NormaId" });
            DropIndex("dbo.Foda", new[] { "NormaId" });
            DropIndex("dbo.FactoresExternos", new[] { "NormaId" });
            DropIndex("dbo.Contexto", new[] { "MapeoId" });
            DropIndex("dbo.Contexto", new[] { "FactoresExternosId" });
            DropIndex("dbo.Contexto", new[] { "AlcanceId" });
            DropIndex("dbo.Contexto", new[] { "NormaId" });
            DropIndex("dbo.AlcanceSistemaGestion", new[] { "NormaId" });
            DropTable("dbo.Document");
            DropTable("dbo.Norma");
            DropTable("dbo.MapeoProcesosInternos");
            DropTable("dbo.Foda");
            DropTable("dbo.FactoresExternos");
            DropTable("dbo.Contexto");
            DropTable("dbo.AlcanceSistemaGestion");
        }
    }
}
