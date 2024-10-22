namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateValidacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CambioProceso", "TipoCambio", c => c.String());
            AddColumn("dbo.CambioProceso", "Responsable", c => c.String());
            AddColumn("dbo.CambioProceso", "FechaCambio", c => c.DateTime(nullable: false));
            AddColumn("dbo.AuditoriaInternaProceso", "FechaAuditoria", c => c.DateTime(nullable: false));
            AddColumn("dbo.AuditoriaInternaProceso", "Responsable", c => c.String());
            DropColumn("dbo.CambioProceso", "Fecha");
            DropColumn("dbo.AuditoriaInternaProceso", "Fecha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditoriaInternaProceso", "Fecha", c => c.DateTime(nullable: false));
            AddColumn("dbo.CambioProceso", "Fecha", c => c.DateTime(nullable: false));
            DropColumn("dbo.AuditoriaInternaProceso", "Responsable");
            DropColumn("dbo.AuditoriaInternaProceso", "FechaAuditoria");
            DropColumn("dbo.CambioProceso", "FechaCambio");
            DropColumn("dbo.CambioProceso", "Responsable");
            DropColumn("dbo.CambioProceso", "TipoCambio");
        }
    }
}
