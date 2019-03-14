namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class media : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "MediaPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "MediaPath");
        }
    }
}
