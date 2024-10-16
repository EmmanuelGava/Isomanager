namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codefirst1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proceso", "ContextoId", c => c.Int(nullable: false));

            // Crear la clave foránea
            AddForeignKey("dbo.Proceso", "ContextoId", "dbo.Contexto", "ContextoId", cascadeDelete: true);
            CreateIndex("dbo.Proceso", "ContextoId");
        }
        
        public override void Down()
        {
        }
    }
}
