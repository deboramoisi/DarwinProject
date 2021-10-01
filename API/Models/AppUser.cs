using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Location { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}