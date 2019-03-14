namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activationCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ActivationCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ActivationCode");
        }
    }
}
