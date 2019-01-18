namespace AlmacenImporting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLocationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocName = c.String(),
                        Notes = c.String(),
                        DateCreated = c.DateTimeOffset(precision: 7),
                        DateUpdated = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Locations");
        }
    }
}
