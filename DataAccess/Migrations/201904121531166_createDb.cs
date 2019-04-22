namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MainPhoto1", c => c.String());
            AddColumn("dbo.Products", "MainPhoto2", c => c.String());
            AddColumn("dbo.Products", "MainPhoto3", c => c.String());
            AddColumn("dbo.Products", "SecondaryPhoto1", c => c.String());
            AddColumn("dbo.Products", "SecondaryPhoto2", c => c.String());
            AddColumn("dbo.Products", "SecondaryPhoto3", c => c.String());
            AddColumn("dbo.Products", "Color", c => c.String());
            DropColumn("dbo.Products", "MainPhoto");
            DropColumn("dbo.Products", "SecondaryPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "SecondaryPhoto", c => c.String());
            AddColumn("dbo.Products", "MainPhoto", c => c.String());
            DropColumn("dbo.Products", "Color");
            DropColumn("dbo.Products", "SecondaryPhoto3");
            DropColumn("dbo.Products", "SecondaryPhoto2");
            DropColumn("dbo.Products", "SecondaryPhoto1");
            DropColumn("dbo.Products", "MainPhoto3");
            DropColumn("dbo.Products", "MainPhoto2");
            DropColumn("dbo.Products", "MainPhoto1");
        }
    }
}
