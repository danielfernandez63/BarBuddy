namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "Name", c => c.String());
            AddColumn("dbo.Recipes", "ReducedFromInventory", c => c.Double(nullable: false));
            AddColumn("dbo.Recipes", "IsSeasonal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "IsSeasonal");
            DropColumn("dbo.Recipes", "ReducedFromInventory");
            DropColumn("dbo.Restaurants", "Name");
        }
    }
}
