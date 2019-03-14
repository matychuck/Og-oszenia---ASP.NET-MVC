namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracja1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        AdID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        DateOfInsert = c.DateTime(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.Int(nullable: false),
                        Password = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ads", "UserID", "dbo.Users");
            DropIndex("dbo.Ads", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Ads");
        }
    }
}
