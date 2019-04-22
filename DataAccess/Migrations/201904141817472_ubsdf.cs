namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ubsdf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ViewCount", c => c.Int());
            AddColumn("dbo.Articles", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Tags");
            DropColumn("dbo.Articles", "ViewCount");
        }
    }
}
