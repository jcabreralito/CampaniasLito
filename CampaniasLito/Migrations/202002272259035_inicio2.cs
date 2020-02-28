namespace CampaniasLito.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicio2 : DbMigration
    {
        public override void Up()
        {
            //CreateIndex("dbo.TiendaArticulos", "TiendaId");
            //AddForeignKey("dbo.TiendaArticulos", "TiendaId", "dbo.Tiendas", "TiendaId");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.TiendaArticulos", "TiendaId", "dbo.Tiendas");
            //DropIndex("dbo.TiendaArticulos", new[] { "TiendaId" });
        }
    }
}
