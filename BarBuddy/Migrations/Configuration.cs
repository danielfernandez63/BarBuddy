namespace BarBuddy.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BarBuddy.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BarBuddy.Models.ApplicationDbContext";
        }

        protected override void Seed(BarBuddy.Models.ApplicationDbContext context)
        {
           // context.Managers.AddOrUpdate(mng => mng.ManagerId,
           // new Models.Manager() { ManagerId = 2, FirstName = "Lara", LastName = "Kroft", PhoneNumber = "16088675309" });

            //context.Bartenders.AddOrUpdate(brt => brt.WorkerId,
            //new Models.Bartender() { WorkerId = 1, FirstName = "Jordy", LastName = "Helsing", DailyTill = 0, PhoneNumber = "16086306751" });

           // context.Restaurants.AddOrUpdate(rst => rst.RestaurantId,
           // new Models.Restaurant() { RestaurantId = 2, Balance = 5000, Name = "The TreeHouse" });

            //context.Recipe.AddOrUpdate(rcp => rcp.RecipeId,
            //new Models.Recipe() { RecipeId = 1, Type = "Vodka", Amount = .50, Price = 5.50 });

            //context.Vodkas.AddOrUpdate(vdk => vdk.VodkaId,
            //new Models.Vodka() { VodkaId = 1, Brand = "Grey Goose", Stock = 6, BottleSize = 750, Price = 6.75 });


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
