namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Activo = c.Boolean(nullable: false),
                        ContextoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaId)
                .ForeignKey("dbo.DefinicionObjetivoAlcance", t => t.ContextoId, cascadeDelete: true)
                .Index(t => t.ContextoId);
            
            CreateTable(
                "dbo.DefinicionObjetivoAlcance",
                c => new
                    {
                        ContextoId = c.Int(nullable: false),
                        Objetivo = c.String(),
                        Alcance = c.String(),
                    })
                .PrimaryKey(t => t.ContextoId)
                .ForeignKey("dbo.Contexto", t => t.ContextoId)
                .Index(t => t.ContextoId);
            
            CreateTable(
                "dbo.Contexto",
                c => new
                    {
                        ContextoId = c.Int(nullable: false, identity: true),
                        NormaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContextoId)
                .ForeignKey("dbo.Norma", t => t.NormaId, cascadeDelete: true)
                .Index(t => t.NormaId);
            
            CreateTable(
                "dbo.FactoresExternos",
                c => new
                    {
                        FactoresExternosId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        FechaCreacion = c.DateTime(nullable: false),
                        TipoFactorId = c.Int(nullable: false),
                        Impacto = c.String(nullable: false),
                        Probabilidad = c.String(nullable: false),
                        AccionesSugeridas = c.String(nullable: false),
                        Responsable = c.String(),
                        ContextoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FactoresExternosId)
                .ForeignKey("dbo.TipoFactor", t => t.TipoFactorId)
                .ForeignKey("dbo.Contexto", t => t.ContextoId)
                .Index(t => t.TipoFactorId)
                .Index(t => t.ContextoId);
            
            CreateTable(
                "dbo.TipoFactor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        ResponsableId = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NormaId)
                .ForeignKey("dbo.Usuarios", t => t.ResponsableId, cascadeDelete: true)
                .Index(t => t.ResponsableId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Rol = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.AuditoriaInternaProceso",
                c => new
                    {
                        AuditoriaId = c.Int(nullable: false, identity: true),
                        FechaAuditoria = c.DateTime(nullable: false),
                        Responsable = c.String(),
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
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .ForeignKey("dbo.Usuarios", t => t.Usuarios_UsuarioId)
                .ForeignKey("dbo.Contexto", t => t.ContextoId)
                .Index(t => t.ContextoId)
                .Index(t => t.UsuarioId)
                .Index(t => t.Usuarios_UsuarioId);
            
            CreateTable(
                "dbo.CambioProceso",
                c => new
                    {
                        CambioId = c.Int(nullable: false, identity: true),
                        TipoCambio = c.String(),
                        Descripcion = c.String(),
                        Responsable = c.String(),
                        FechaCambio = c.DateTime(nullable: false),
                        ProcesoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CambioId)
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
                        AreaMejora = c.String(),
                        AccionRecomendada = c.String(),
                        Responsable = c.String(),
                        FechaImplementacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.MejoraId)
                .ForeignKey("dbo.Proceso", t => t.ProcesoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.ProcesoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.UbicacionGeografica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        DefinicionObjetivoAlcanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DefinicionObjetivoAlcance", t => t.DefinicionObjetivoAlcanceId, cascadeDelete: true)
                .Index(t => t.DefinicionObjetivoAlcanceId);
            
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
                "dbo.EvaluacionProceso",
                c => new
                    {
                        EvaluacionId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Observaciones = c.String(),
                        IndicadoresClaves = c.String(),
                        ProcesoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EvaluacionId)
                .ForeignKey("dbo.Proceso", t => t.ProcesoId, cascadeDelete: true)
                .Index(t => t.ProcesoId);
            
            CreateTable(
                "dbo.KPI",
                c => new
                    {
                        KpiId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Valor = c.String(),
                        FechaMedicion = c.DateTime(nullable: false),
                        ProcesoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KpiId)
                .ForeignKey("dbo.Proceso", t => t.ProcesoId, cascadeDelete: true)
                .Index(t => t.ProcesoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KPI", "ProcesoId", "dbo.Proceso");
            DropForeignKey("dbo.EvaluacionProceso", "ProcesoId", "dbo.Proceso");
            DropForeignKey("dbo.UbicacionGeografica", "DefinicionObjetivoAlcanceId", "dbo.DefinicionObjetivoAlcance");
            DropForeignKey("dbo.Proceso", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Norma", "ResponsableId", "dbo.Usuarios");
            DropForeignKey("dbo.Proceso", "Usuarios_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.MejoraProceso", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.CambioProceso", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.AuditoriaInternaProceso", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.AuditoriaInternaProceso", "ProcesoId", "dbo.Proceso");
            DropForeignKey("dbo.Proceso", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.MejoraProceso", "ProcesoId", "dbo.Proceso");
            DropForeignKey("dbo.CambioProceso", "ProcesoId", "dbo.Proceso");
            DropForeignKey("dbo.Contexto", "NormaId", "dbo.Norma");
            DropForeignKey("dbo.Foda", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.FactoresExternos", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.FactoresExternos", "TipoFactorId", "dbo.TipoFactor");
            DropForeignKey("dbo.DefinicionObjetivoAlcance", "ContextoId", "dbo.Contexto");
            DropForeignKey("dbo.Area", "ContextoId", "dbo.DefinicionObjetivoAlcance");
            DropIndex("dbo.KPI", new[] { "ProcesoId" });
            DropIndex("dbo.EvaluacionProceso", new[] { "ProcesoId" });
            DropIndex("dbo.UbicacionGeografica", new[] { "DefinicionObjetivoAlcanceId" });
            DropIndex("dbo.MejoraProceso", new[] { "UsuarioId" });
            DropIndex("dbo.MejoraProceso", new[] { "ProcesoId" });
            DropIndex("dbo.CambioProceso", new[] { "UsuarioId" });
            DropIndex("dbo.CambioProceso", new[] { "ProcesoId" });
            DropIndex("dbo.Proceso", new[] { "Usuarios_UsuarioId" });
            DropIndex("dbo.Proceso", new[] { "UsuarioId" });
            DropIndex("dbo.Proceso", new[] { "ContextoId" });
            DropIndex("dbo.AuditoriaInternaProceso", new[] { "UsuarioId" });
            DropIndex("dbo.AuditoriaInternaProceso", new[] { "ProcesoId" });
            DropIndex("dbo.Norma", new[] { "ResponsableId" });
            DropIndex("dbo.Foda", new[] { "ContextoId" });
            DropIndex("dbo.FactoresExternos", new[] { "ContextoId" });
            DropIndex("dbo.FactoresExternos", new[] { "TipoFactorId" });
            DropIndex("dbo.Contexto", new[] { "NormaId" });
            DropIndex("dbo.DefinicionObjetivoAlcance", new[] { "ContextoId" });
            DropIndex("dbo.Area", new[] { "ContextoId" });
            DropTable("dbo.KPI");
            DropTable("dbo.EvaluacionProceso");
            DropTable("dbo.Document");
            DropTable("dbo.UbicacionGeografica");
            DropTable("dbo.MejoraProceso");
            DropTable("dbo.CambioProceso");
            DropTable("dbo.Proceso");
            DropTable("dbo.AuditoriaInternaProceso");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Norma");
            DropTable("dbo.Foda");
            DropTable("dbo.TipoFactor");
            DropTable("dbo.FactoresExternos");
            DropTable("dbo.Contexto");
            DropTable("dbo.DefinicionObjetivoAlcance");
            DropTable("dbo.Area");
        }
    }
}
