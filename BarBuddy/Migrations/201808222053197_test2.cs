namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "Type", c => c.String());
            AddColumn("dbo.Inventories", "Stock", c => c.Double(nullable: false));
            AddColumn("dbo.Inventories", "BottleSize", c => c.Double(nullable: false));
            AddColumn("dbo.Inventories", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "Price");
            DropColumn("dbo.Inventories", "BottleSize");
            DropColumn("dbo.Inventories", "Stock");
            DropColumn("dbo.Inventories", "Type");
        }
    }
}
