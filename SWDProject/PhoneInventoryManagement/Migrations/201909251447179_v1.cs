namespace PhoneInventoryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bill",
                c => new
                    {
                        BillId = c.Guid(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false),
                        BillType = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Guid(nullable: false),
                        WareHouseId = c.Guid(nullable: false),
                        OrderId = c.Guid(),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.WareHouse", t => t.WareHouseId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WareHouseId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Guid(nullable: false, identity: true),
                        CustomerName = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderDetailId = c.Guid(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        SalePrice = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Guid(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductType = c.String(),
                        Manufacturer = c.String(),
                        Color = c.String(),
                        Description = c.String(),
                        ImportPrice = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductItem",
                c => new
                    {
                        ProductItemId = c.Guid(nullable: false, identity: true),
                        IMEI = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        BillId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductItemId)
                .ForeignKey("dbo.Bill", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Guid(nullable: false, identity: true),
                        RoleName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.WareHouse",
                c => new
                    {
                        WareHouseId = c.Guid(nullable: false, identity: true),
                        WareHouseName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WareHouseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bill", "WareHouseId", "dbo.WareHouse");
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Order", "UserId", "dbo.User");
            DropForeignKey("dbo.Bill", "UserId", "dbo.User");
            DropForeignKey("dbo.ProductItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductItem", "BillId", "dbo.Bill");
            DropForeignKey("dbo.OrderDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Bill", "OrderId", "dbo.Order");
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.ProductItem", new[] { "BillId" });
            DropIndex("dbo.ProductItem", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "UserId" });
            DropIndex("dbo.Bill", new[] { "OrderId" });
            DropIndex("dbo.Bill", new[] { "WareHouseId" });
            DropIndex("dbo.Bill", new[] { "UserId" });
            DropTable("dbo.WareHouse");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.ProductItem");
            DropTable("dbo.Product");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.Bill");
        }
    }
}
