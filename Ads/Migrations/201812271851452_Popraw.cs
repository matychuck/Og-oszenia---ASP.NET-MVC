namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Popraw : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdminMessages", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.AdminMessages", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdminMessages", "Content", c => c.String());
            AlterColumn("dbo.AdminMessages", "Title", c => c.String());
        }
    }
}
