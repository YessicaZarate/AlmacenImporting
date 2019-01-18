namespace AlmacenImporting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLocationsTableAndLinks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "LocId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "LocId");
            AddForeignKey("dbo.Products", "LocId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "LocId", "dbo.Locations");
            DropIndex("dbo.Products", new[] { "LocId" });
            DropColumn("dbo.Products", "LocId");
        }
    }
}
