using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Context.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string CompanyName { get; set; }
        public virtual ICollection<Home> Homes { get; set; }
    }
}
