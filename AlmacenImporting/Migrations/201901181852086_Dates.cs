namespace AlmacenImporting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DateCreated", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Products", "DateUpdated", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Providers", "DateCreated", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Providers", "DateUpdated", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Providers", "DateUpdated");
            DropColumn("dbo.Providers", "DateCreated");
            DropColumn("dbo.Products", "DateUpdated");
            DropColumn("dbo.Products", "DateCreated");
        }
    }
}
