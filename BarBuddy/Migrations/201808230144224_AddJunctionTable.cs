namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJunctionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TabRecipes",
                c => new
                    {
                        TabRecipe = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(),
                        TabId = c.Int(),
                    })
                .PrimaryKey(t => t.TabRecipe)
                .ForeignKey("dbo.Recipes", t => t.RecipeId)
                .ForeignKey("dbo.Tabs", t => t.TabId)
                .Index(t => t.RecipeId)
                .Index(t => t.TabId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TabRecipes", "TabId", "dbo.Tabs");
            DropForeignKey("dbo.TabRecipes", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.TabRecipes", new[] { "TabId" });
            DropIndex("dbo.TabRecipes", new[] { "RecipeId" });
            DropTable("dbo.TabRecipes");
        }
    }
}
