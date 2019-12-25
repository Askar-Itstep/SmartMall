namespace SmartMall.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fullname_customer = c.String(nullable: false, maxLength: 50),
                        age_custom = c.Int(nullable: false),
                        login_custom = c.String(nullable: false, maxLength: 50),
                        password_custom = c.String(nullable: false, maxLength: 50),
                        role_id = c.Int(),
                        address = c.String(unicode: false, storeType: "text"),
                        phoneNum = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Roles", t => t.role_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.Debitors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        custom_id = c.Int(),
                        order_id = c.Int(),
                        sum_begin_debit = c.Decimal(storeType: "money"),
                        new_pay = c.Decimal(storeType: "money"),
                        date_new_pay = c.DateTime(storeType: "date"),
                        current_sum_debit = c.Decimal(storeType: "money"),
                        date_plan_repay = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Orders", t => t.order_id)
                .ForeignKey("dbo.Customers", t => t.custom_id)
                .Index(t => t.custom_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        custom_id = c.Int(),
                        prod_id = c.Int(),
                        number_item = c.Int(nullable: false),
                        date_ship = c.DateTime(nullable: false, storeType: "date"),
                        sum_pay = c.Decimal(nullable: false, storeType: "money"),
                        sum_order = c.Decimal(nullable: false, storeType: "money"),
                        sum_debit = c.Decimal(storeType: "money"),
                        seller_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Employees", t => t.seller_id)
                .ForeignKey("dbo.Products", t => t.prod_id)
                .ForeignKey("dbo.Customers", t => t.custom_id)
                .Index(t => t.custom_id)
                .Index(t => t.prod_id)
                .Index(t => t.seller_id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fullname_emp = c.String(nullable: false, maxLength: 50),
                        age_emp = c.Int(nullable: false),
                        login_emp = c.String(nullable: false, maxLength: 50),
                        password_emp = c.String(nullable: false, maxLength: 50),
                        role_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Roles", t => t.role_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name_role = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name_prod = c.String(nullable: false, maxLength: 150),
                        manufactur = c.String(maxLength: 50),
                        model = c.String(maxLength: 50),
                        imagePath = c.String(maxLength: 200),
                        quantity_on_storage = c.Int(),
                        purch_price = c.Decimal(nullable: false, storeType: "money"),
                        price = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.StatisticSeller",
                c => new
                    {
                        fullname_emp = c.String(nullable: false, maxLength: 50),
                        num_sell = c.Int(),
                        sum_cash = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.fullname_emp);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "custom_id", "dbo.Customers");
            DropForeignKey("dbo.Debitors", "custom_id", "dbo.Customers");
            DropForeignKey("dbo.Orders", "prod_id", "dbo.Products");
            DropForeignKey("dbo.Employees", "role_id", "dbo.Roles");
            DropForeignKey("dbo.Customers", "role_id", "dbo.Roles");
            DropForeignKey("dbo.Orders", "seller_id", "dbo.Employees");
            DropForeignKey("dbo.Debitors", "order_id", "dbo.Orders");
            DropIndex("dbo.Employees", new[] { "role_id" });
            DropIndex("dbo.Orders", new[] { "seller_id" });
            DropIndex("dbo.Orders", new[] { "prod_id" });
            DropIndex("dbo.Orders", new[] { "custom_id" });
            DropIndex("dbo.Debitors", new[] { "order_id" });
            DropIndex("dbo.Debitors", new[] { "custom_id" });
            DropIndex("dbo.Customers", new[] { "role_id" });
            DropTable("dbo.StatisticSeller");
            DropTable("dbo.Products");
            DropTable("dbo.Roles");
            DropTable("dbo.Employees");
            DropTable("dbo.Orders");
            DropTable("dbo.Debitors");
            DropTable("dbo.Customers");
        }
    }
}
