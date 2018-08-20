namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedFewErrorsInOriginalModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Managers", name: "ResturantId", newName: "RestaurantId");
            RenameIndex(table: "dbo.Managers", name: "IX_ResturantId", newName: "IX_RestaurantId");
            AddColumn("dbo.Recipes", "Name", c => c.String());
            AddColumn("dbo.Recipes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Description");
            DropColumn("dbo.Recipes", "Name");
            RenameIndex(table: "dbo.Managers", name: "IX_RestaurantId", newName: "IX_ResturantId");
            RenameColumn(table: "dbo.Managers", name: "RestaurantId", newName: "ResturantId");
        }
    }
}
