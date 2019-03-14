namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuwanieSmieci : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mailboxes", "mailmessage_MailMessageID", "dbo.MailMessages");
            DropForeignKey("dbo.Mailboxes", "User_UserID", "dbo.Users");
            DropIndex("dbo.Mailboxes", new[] { "mailmessage_MailMessageID" });
            DropIndex("dbo.Mailboxes", new[] { "User_UserID" });
            DropTable("dbo.Mailboxes");
            DropTable("dbo.MailMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MailMessages",
                c => new
                    {
                        MailMessageID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.MailMessageID);
            
            CreateTable(
                "dbo.Mailboxes",
                c => new
                    {
                        MailboxID = c.Int(nullable: false, identity: true),
                        mailmessage_MailMessageID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.MailboxID);
            
            CreateIndex("dbo.Mailboxes", "User_UserID");
            CreateIndex("dbo.Mailboxes", "mailmessage_MailMessageID");
            AddForeignKey("dbo.Mailboxes", "User_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Mailboxes", "mailmessage_MailMessageID", "dbo.MailMessages", "MailMessageID");
        }
    }
}
