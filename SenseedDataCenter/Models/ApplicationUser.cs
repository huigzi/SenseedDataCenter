using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SenseedDataCenter.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<IdentityUserRole<string>> Roles { get; set; }
    }
}
