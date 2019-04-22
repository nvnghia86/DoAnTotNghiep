namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asw : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceOrderRepairs", "Service_ServicerId", "dbo.Services");
            DropForeignKey("dbo.ServiceOrderRepairs", "OrderRepair_OrderRepairId", "dbo.OrderRepairs");
            DropForeignKey("dbo.OrderRepairs", "CustomerId", "dbo.Users");
            DropIndex("dbo.OrderRepairs", new[] { "CustomerId" });
            DropIndex("dbo.ServiceOrderRepairs", new[] { "Service_ServicerId" });
            DropIndex("dbo.ServiceOrderRepairs", new[] { "OrderRepair_OrderRepairId" });
            AddColumn("dbo.OrderRepairs", "CustomerName", c => c.String());
            AddColumn("dbo.OrderRepairs", "PhoneNumber", c => c.String());
            AddColumn("dbo.OrderRepairs", "Customermail", c => c.String());
            DropColumn("dbo.OrderRepairs", "CustomerId");
            DropColumn("dbo.OrderRepairs", "ServiceId");
            DropColumn("dbo.OrderRepairs", "Status");
            DropTable("dbo.ServiceOrderRepairs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceOrderRepairs",
                c => new
                    {
                        Service_ServicerId = c.Int(nullable: false),
                        OrderRepair_OrderRepairId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ServicerId, t.OrderRepair_OrderRepairId });
            
            AddColumn("dbo.OrderRepairs", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderRepairs", "ServiceId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderRepairs", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderRepairs", "Customermail");
            DropColumn("dbo.OrderRepairs", "PhoneNumber");
            DropColumn("dbo.OrderRepairs", "CustomerName");
            CreateIndex("dbo.ServiceOrderRepairs", "OrderRepair_OrderRepairId");
            CreateIndex("dbo.ServiceOrderRepairs", "Service_ServicerId");
            CreateIndex("dbo.OrderRepairs", "CustomerId");
            AddForeignKey("dbo.OrderRepairs", "CustomerId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.ServiceOrderRepairs", "OrderRepair_OrderRepairId", "dbo.OrderRepairs", "OrderRepairId", cascadeDelete: true);
            AddForeignKey("dbo.ServiceOrderRepairs", "Service_ServicerId", "dbo.Services", "ServicerId", cascadeDelete: true);
        }
    }
}
