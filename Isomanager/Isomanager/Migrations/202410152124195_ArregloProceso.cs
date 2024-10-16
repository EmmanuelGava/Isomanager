namespace Isomanager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ArregloProceso : DbMigration
    {
        public override void Up()
        {
            // Eliminar la clave foránea primero
            DropForeignKey("dbo.Proceso", "ContextoId", "dbo.Contexto");

            // Eliminar el índice que depende de la columna
            DropIndex("dbo.Proceso", new[] { "ContextoId" });

            // Ahora puedes eliminar la columna
            DropColumn("dbo.Proceso", "ContextoId");

            // Realiza los cambios de renombrado
            RenameColumn(table: "dbo.Contexto", name: "AlcanceId", newName: "AlcanceSistemaGestion_AlcanceId");
            RenameColumn(table: "dbo.Proceso", name: "Contexto_ContextoId", newName: "ContextoId");
            RenameIndex(table: "dbo.Contexto", name: "IX_AlcanceId", newName: "IX_AlcanceSistemaGestion_AlcanceId");

            // Agregar la clave foránea nuevamente
            AddForeignKey("dbo.Proceso", "ContextoId", "dbo.Contexto", "ContextoId");
        }

        public override void Down()
        {
            // Invertir los cambios en el método Down
            DropForeignKey("dbo.Proceso", "ContextoId", "dbo.Contexto");
            RenameIndex(table: "dbo.Contexto", name: "IX_AlcanceSistemaGestion_AlcanceId", newName: "IX_AlcanceId");
            RenameColumn(table: "dbo.Proceso", name: "ContextoId", newName: "Contexto_ContextoId");
            RenameColumn(table: "dbo.Contexto", name: "AlcanceSistemaGestion_AlcanceId", newName: "AlcanceId");
            AddColumn("dbo.Proceso", "ContextoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Proceso", "Contexto_ContextoId");
            AddForeignKey("dbo.Proceso", "ContextoId", "dbo.Contexto", "ContextoId", cascadeDelete: true);
        }
    }
}
