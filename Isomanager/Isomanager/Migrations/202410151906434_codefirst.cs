namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codefirst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contexto", "FodaId", c => c.Int(nullable: true));
            CreateIndex("dbo.Contexto", "FodaId");
            AddForeignKey("dbo.Contexto", "FodaId", "dbo.Foda", "ContextoId");
        }
        
        public override void Down()
        {
        }
    }
}
