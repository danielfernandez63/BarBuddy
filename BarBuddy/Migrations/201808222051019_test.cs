namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Inventories", "Type");
            DropColumn("dbo.Inventories", "Stock");
            DropColumn("dbo.Inventories", "BottleSize");
            DropColumn("dbo.Inventories", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Inventories", "BottleSize", c => c.Double(nullable: false));
            AddColumn("dbo.Inventories", "Stock", c => c.Double(nullable: false));
            AddColumn("dbo.Inventories", "Type", c => c.String());
        }
    }
}
