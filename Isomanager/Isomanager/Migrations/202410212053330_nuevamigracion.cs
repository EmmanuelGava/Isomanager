namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuevamigracion : DbMigration
    {
        public override void Up()
        {
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
            DropIndex("dbo.KPI", new[] { "ProcesoId" });
            DropIndex("dbo.EvaluacionProceso", new[] { "ProcesoId" });
            DropTable("dbo.KPI");
            DropTable("dbo.EvaluacionProceso");
        }
    }
}
