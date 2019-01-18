namespace AlmacenImporting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProdId = c.Int(nullable: false, identity: true),
                        Item = c.String(),
                        Description = c.String(),
                        Qty = c.Int(nullable: false),
                        Cost = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        ProvId = c.Int(nullable: false),
                        Warranty = c.Int(nullable: false),
                        DateAd = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ProdId)
                .ForeignKey("dbo.Providers", t => t.ProvId, cascadeDelete: true)
                .Index(t => t.ProvId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProvName = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProvId", "dbo.Providers");
            DropIndex("dbo.Products", new[] { "ProvId" });
            DropTable("dbo.Providers");
            DropTable("dbo.Products");
        }
    }
}
