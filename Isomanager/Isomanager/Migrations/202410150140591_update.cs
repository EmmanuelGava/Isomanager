namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
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
            DropTable("dbo.Usuario");
            DropTable("dbo.Mejora");
        }
    }
}
