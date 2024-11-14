namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormacionYDesempeno : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Desempeno",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mes = c.String(),
                        Promedio = c.Double(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Formacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Area = c.String(),
                        Horas = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Formacion", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Desempeno", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Formacion", new[] { "UsuarioId" });
            DropIndex("dbo.Desempeno", new[] { "UsuarioId" });
            DropTable("dbo.Formacion");
            DropTable("dbo.Desempeno");
        }
    }
}
