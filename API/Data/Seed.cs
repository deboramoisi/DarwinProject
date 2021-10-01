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
        public static async Task SeedRoles(RoleManager<AppRole> roleManager) 
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
        }
    }
}