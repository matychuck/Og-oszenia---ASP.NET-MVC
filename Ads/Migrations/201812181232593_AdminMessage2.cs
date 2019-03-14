namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminMessage2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminMessages",
                c => new
                    {
                        AdminMessageID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.AdminMessageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminMessages");
        }
    }
}
