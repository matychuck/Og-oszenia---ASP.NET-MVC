namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Done : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attributes", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attributes", "Value");
        }
    }
}
