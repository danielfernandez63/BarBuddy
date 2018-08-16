using BarBuddy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BarBuddy.Startup))]
namespace BarBuddy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }



        private void createRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!RoleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                RoleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "BruceBanner";
                user.Email = "danmanfernan@gmail.com";

                string userPWD = "D3vC0d3$tud3nt";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!RoleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                RoleManager.Create(role);
            }


            if (!RoleManager.RoleExists("Bartender"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Bartender";
                RoleManager.Create(role);
            }

           

        }

    }
}

