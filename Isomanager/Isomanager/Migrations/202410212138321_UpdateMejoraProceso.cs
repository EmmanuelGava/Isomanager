namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMejoraProceso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MejoraProceso", "AreaMejora", c => c.String());
            AddColumn("dbo.MejoraProceso", "AccionRecomendada", c => c.String());
            AddColumn("dbo.MejoraProceso", "Responsable", c => c.String());
            AddColumn("dbo.MejoraProceso", "FechaImplementacion", c => c.DateTime());
            DropTable("dbo.Mejora");
        }
        
        public override void Down()
        {
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
            
            DropColumn("dbo.MejoraProceso", "FechaImplementacion");
            DropColumn("dbo.MejoraProceso", "Responsable");
            DropColumn("dbo.MejoraProceso", "AccionRecomendada");
            DropColumn("dbo.MejoraProceso", "AreaMejora");
        }
    }
}
