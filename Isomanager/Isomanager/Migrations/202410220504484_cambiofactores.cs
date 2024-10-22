namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiofactores : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FactoresExternos", "TipoFactor", c => c.String(nullable: false));
            AddColumn("dbo.FactoresExternos", "Impacto", c => c.String(nullable: false));
            AddColumn("dbo.FactoresExternos", "Probabilidad", c => c.String(nullable: false));
            AddColumn("dbo.FactoresExternos", "AccionesSugeridas", c => c.String(nullable: false));
            AddColumn("dbo.FactoresExternos", "Responsable", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FactoresExternos", "Responsable");
            DropColumn("dbo.FactoresExternos", "AccionesSugeridas");
            DropColumn("dbo.FactoresExternos", "Probabilidad");
            DropColumn("dbo.FactoresExternos", "Impacto");
            DropColumn("dbo.FactoresExternos", "TipoFactor");
        }
    }
}
