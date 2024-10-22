namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUsuarioIdNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Proceso", "UsuarioId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proceso", "UsuarioId", c => c.Int(nullable: false));
        }
    }
}
