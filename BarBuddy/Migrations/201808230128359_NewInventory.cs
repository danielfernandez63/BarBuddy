namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInventory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bartenders", "TabId", "dbo.Tabs");
            DropIndex("dbo.Bartenders", new[] { "TabId" });
            AddColumn("dbo.Tabs", "Name", c => c.String());
            AddColumn("dbo.Tabs", "WorkerId", c => c.Int());
            CreateIndex("dbo.Tabs", "WorkerId");
            AddForeignKey("dbo.Tabs", "WorkerId", "dbo.Bartenders", "WorkerId");
            DropColumn("dbo.Bartenders", "TabId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bartenders", "TabId", c => c.Int());
            DropForeignKey("dbo.Tabs", "WorkerId", "dbo.Bartenders");
            DropIndex("dbo.Tabs", new[] { "WorkerId" });
            DropColumn("dbo.Tabs", "WorkerId");
            DropColumn("dbo.Tabs", "Name");
            CreateIndex("dbo.Bartenders", "TabId");
            AddForeignKey("dbo.Bartenders", "TabId", "dbo.Tabs", "TabId");
        }
    }
}
