using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SenseedDataCenter.Models
{
    public static class SeedData
    {
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
