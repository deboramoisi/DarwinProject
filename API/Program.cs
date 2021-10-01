using System;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
            try 
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedRoles(roleManager);
            }
            catch (Exception ex) 
            {
                var logger = services.GetRequiredService<ILogger>();
                logger.LogError(ex, "An error occured during DB seeding");
            }

            // var admin = new AppUser {
            //     UserName = "admin"
            // };

            // var userManager = services.GetRequiredService<UserManager<AppUser>>();
            // await userManager.CreateAsync(admin, "P@$$w0rd");
            // await userManager.AddToRoleAsync(admin, "Admin");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
