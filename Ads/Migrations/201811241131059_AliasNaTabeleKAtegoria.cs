namespace Ads.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AliasNaTabeleKAtegoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ParentcategoryID", c => c.Int());
            AddColumn("dbo.Categories", "CategoryParent_CategoryID", c => c.Int());
            CreateIndex("dbo.Categories", "CategoryParent_CategoryID");
            AddForeignKey("dbo.Categories", "CategoryParent_CategoryID", "dbo.Categories", "CategoryID");
            DropColumn("dbo.Categories", "SubcategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "SubcategoryID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Categories", "CategoryParent_CategoryID", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "CategoryParent_CategoryID" });
            DropColumn("dbo.Categories", "CategoryParent_CategoryID");
            DropColumn("dbo.Categories", "ParentcategoryID");
        }
    }
}
