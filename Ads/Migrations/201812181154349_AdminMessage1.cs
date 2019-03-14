namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminMessage1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "VisitCounter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "VisitCounter");
        }
    }
}
