using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SenseedDataCenter.Models
{
    public static class SeedData
    {
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPw,
            string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new IdentityUser {UserName = userName};
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult identityResult = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            identityResult = await userManager.AddToRoleAsync(user, role);

            return identityResult;

        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new SenseedDataCenterContext(serviceProvider
                    .GetRequiredService<DbContextOptions<SenseedDataCenterContext>>()))
            {
                if (context.Categories.Any())
                {
                    return;
                }

                context.Categories.AddRange(
                    new Category
                    {
                        Name = "风速仪（485接口）"
                    },

                    new Category
                    {
                        Name = "风速仪（232接口）"
                    }

                    );

                context.SaveChanges();
            }
        }

    }
}
