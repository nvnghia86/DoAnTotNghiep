namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ubs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleTags",
                c => new
                    {
                        ContentID = c.Long(nullable: false),
                        TagID = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ContentID, t.TagID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ArticleTags");
        }
    }
}
