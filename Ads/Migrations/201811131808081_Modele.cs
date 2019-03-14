namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modele : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SubcategoryID = c.Int(nullable: false),
                        Attribute_AttributeID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Attributes", t => t.Attribute_AttributeID)
                .Index(t => t.Attribute_AttributeID);
            
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        AttributeID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AttributeID);
            
            CreateTable(
                "dbo.Mailboxes",
                c => new
                    {
                        MailboxID = c.Int(nullable: false, identity: true),
                        mailmessage_MailMessageID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.MailboxID)
                .ForeignKey("dbo.MailMessages", t => t.mailmessage_MailMessageID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.mailmessage_MailMessageID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.MailMessages",
                c => new
                    {
                        MailMessageID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.MailMessageID);
            
            CreateIndex("dbo.Ads", "CategoryID");
            AddForeignKey("dbo.Ads", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mailboxes", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Mailboxes", "mailmessage_MailMessageID", "dbo.MailMessages");
            DropForeignKey("dbo.Categories", "Attribute_AttributeID", "dbo.Attributes");
            DropForeignKey("dbo.Ads", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Mailboxes", new[] { "User_UserID" });
            DropIndex("dbo.Mailboxes", new[] { "mailmessage_MailMessageID" });
            DropIndex("dbo.Categories", new[] { "Attribute_AttributeID" });
            DropIndex("dbo.Ads", new[] { "CategoryID" });
            DropTable("dbo.MailMessages");
            DropTable("dbo.Mailboxes");
            DropTable("dbo.Attributes");
            DropTable("dbo.Categories");
        }
    }
}
