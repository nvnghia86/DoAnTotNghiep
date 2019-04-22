namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asw33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderRepairs", "CustomerAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderRepairs", "CustomerAddress");
        }
    }
}
