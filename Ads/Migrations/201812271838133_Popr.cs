namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Popr : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Attributes", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attributes", "Value", c => c.String());
        }
    }
}
