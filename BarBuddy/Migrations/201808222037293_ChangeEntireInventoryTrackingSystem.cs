namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEntireInventoryTrackingSystem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "Type", c => c.String());
            AddColumn("dbo.Inventories", "Stock", c => c.Double(nullable: false));
            AddColumn("dbo.Inventories", "BottleSize", c => c.Double(nullable: false));
            AddColumn("dbo.Inventories", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Recipes", "InventoryId", c => c.Int());
            CreateIndex("dbo.Recipes", "InventoryId");
            AddForeignKey("dbo.Recipes", "InventoryId", "dbo.Inventories", "InventoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Recipes", new[] { "InventoryId" });
            DropColumn("dbo.Recipes", "InventoryId");
            DropColumn("dbo.Inventories", "Price");
            DropColumn("dbo.Inventories", "BottleSize");
            DropColumn("dbo.Inventories", "Stock");
            DropColumn("dbo.Inventories", "Type");
        }
    }
}
