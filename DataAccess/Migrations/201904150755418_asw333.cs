namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asw333 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderRepairs", "CustomerEmail", c => c.String(nullable: false));
            AlterColumn("dbo.OrderRepairs", "CustomerName", c => c.String(nullable: false));
            AlterColumn("dbo.OrderRepairs", "CustomerAddress", c => c.String(nullable: false));
            AlterColumn("dbo.OrderRepairs", "Content", c => c.String(nullable: false));
            DropColumn("dbo.OrderRepairs", "Customermail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderRepairs", "Customermail", c => c.String());
            AlterColumn("dbo.OrderRepairs", "Content", c => c.String());
            AlterColumn("dbo.OrderRepairs", "CustomerAddress", c => c.String());
            AlterColumn("dbo.OrderRepairs", "CustomerName", c => c.String());
            DropColumn("dbo.OrderRepairs", "CustomerEmail");
        }
    }
}
