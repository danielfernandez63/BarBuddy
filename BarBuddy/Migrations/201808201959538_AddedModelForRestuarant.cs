namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModelForRestuarant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bartenders",
                c => new
                    {
                        WorkerId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        RestaurantId = c.Int(),
                        TabId = c.Int(),
                        ManagerId = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        DailyTill = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.WorkerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .ForeignKey("dbo.Tabs", t => t.TabId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.RestaurantId)
                .Index(t => t.TabId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        ResturantId = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ManagerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Restaurants", t => t.ResturantId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ResturantId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Tabs",
                c => new
                    {
                        TabId = c.Int(nullable: false, identity: true),
                        Total = c.Double(nullable: false),
                        CheckOut = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TabId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(),
                        Type = c.String(),
                        Amount = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Vodkas",
                c => new
                    {
                        VodkaId = c.Int(nullable: false, identity: true),
                        InventoryId = c.Int(),
                        Stock = c.Double(nullable: false),
                        Brand = c.String(),
                        BottleSize = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VodkaId)
                .ForeignKey("dbo.Inventories", t => t.InventoryId)
                .Index(t => t.InventoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vodkas", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.Recipes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Inventories", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Bartenders", "TabId", "dbo.Tabs");
            DropForeignKey("dbo.Bartenders", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Bartenders", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Managers", "ResturantId", "dbo.Restaurants");
            DropForeignKey("dbo.Managers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bartenders", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Vodkas", new[] { "InventoryId" });
            DropIndex("dbo.Recipes", new[] { "RestaurantId" });
            DropIndex("dbo.Inventories", new[] { "RestaurantId" });
            DropIndex("dbo.Managers", new[] { "ResturantId" });
            DropIndex("dbo.Managers", new[] { "ApplicationUserId" });
            DropIndex("dbo.Bartenders", new[] { "ManagerId" });
            DropIndex("dbo.Bartenders", new[] { "TabId" });
            DropIndex("dbo.Bartenders", new[] { "RestaurantId" });
            DropIndex("dbo.Bartenders", new[] { "ApplicationUserId" });
            DropTable("dbo.Vodkas");
            DropTable("dbo.Recipes");
            DropTable("dbo.Inventories");
            DropTable("dbo.Tabs");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Managers");
            DropTable("dbo.Bartenders");
        }
    }
}
