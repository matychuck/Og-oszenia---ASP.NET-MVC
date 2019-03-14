namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adsDataFW : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForbiddenWords",
                c => new
                    {
                        ForbiddenWordID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ForbiddenWordID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ForbiddenWords");
        }
    }
}
