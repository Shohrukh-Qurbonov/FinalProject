using FinalProject.Context.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Seeds
{
    public class DefaultUsers
    {
        public static async Task AddDefaultUsers(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.FindByNameAsync("Admin");
            if(admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@admin.ru"
                };
                await userManager.CreateAsync(admin, "@dmin123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
