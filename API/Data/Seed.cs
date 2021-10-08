using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedRoles(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager) 
        {
            if (await roleManager.Roles.AnyAsync()) return;

            var roles = new List<AppRole> 
            {
                new AppRole { Name = "Member" },
                new AppRole { Name = "Admin" }
            };

            foreach (AppRole role in roles) 
            {
                await roleManager.CreateAsync(role);
            }

            // create an admin
            var admin = new AppUser 
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com"
            };

            await userManager.CreateAsync(admin, "D@rwin");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}