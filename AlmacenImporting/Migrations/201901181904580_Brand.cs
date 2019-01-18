namespace AlmacenImporting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Brand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Brand", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Brand");
        }
    }
}
