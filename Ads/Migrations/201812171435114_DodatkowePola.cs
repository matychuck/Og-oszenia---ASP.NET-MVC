namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodatkowePola : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "IsModerated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Attributes", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Users", "MyPagination", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "MyPagination");
            DropColumn("dbo.Attributes", "Name");
            DropColumn("dbo.Ads", "IsModerated");
        }
    }
}
