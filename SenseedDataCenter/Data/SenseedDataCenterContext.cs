using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SenseedDataCenter.ViewModel;

namespace SenseedDataCenter.Models
{
    public class SenseedDataCenterContext: IdentityDbContext
    {
        public SenseedDataCenterContext (DbContextOptions<SenseedDataCenterContext> options)
            : base(options)
        {
        }

        public DbSet<SenseedDataCenter.Models.AnemometerRecord> AnemometerRecords { get; set; }

        public DbSet<SenseedDataCenter.Models.Category> Categories { get; set; }

        public DbSet<SenseedDataCenter.Models.Product> Products { get; set; }

    }
}
