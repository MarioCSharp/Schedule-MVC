using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schedule_MVC.Data;
using Schedule_MVC.Data.Models;

namespace Schedule_MVC.Infrastructure
{
    public static class ApplicationStartUp
    {
        public static IApplicationBuilder PrepareApp(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            MigrateDatabase(services);
            SeedAdministrator(services);
            return app;
        }
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();
            data.Database.Migrate();
        }
        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }
                    var role = new IdentityRole
                    {
                        Name = "Administrator"
                    };
                    await roleManager.CreateAsync(role);
                    const string adminEmail = "mario.18@abv.bg";
                    const string adminPassword = "Admin?12";
                    var user = new User
                    {
                        Email = adminEmail,
                        FullName = "Mario Petkov",
                        UserName = adminEmail
                    };
                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
