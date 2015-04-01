namespace NewsSite.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using NewsSite.Constants;
    using NewsSite.Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NewsSiteDbContext>
    {
        private UserManager<User> userManager;
        private UserStore<User> userStore;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(NewsSiteDbContext context)
        {
          
            this.userStore = new UserStore<User>(context);
            this.userManager = new UserManager<User>(this.userStore);

            this.SeedRoles(context);
            this.SeedAdmin(context);
        }

        private void SeedRoles(NewsSiteDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdminRole));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.ReporterRole));
            context.SaveChanges();
        }

        private void SeedAdmin(NewsSiteDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var admin = new User
            {
                Email = GlobalConstants.AdminEmailUsername,
                UserName = GlobalConstants.AdminEmailUsername,
            };

            this.userManager.Create(admin, GlobalConstants.AdminPassword);
            this.userManager.AddToRole(admin.Id, GlobalConstants.AdminRole);
            context.SaveChanges();
        }
    }
}
