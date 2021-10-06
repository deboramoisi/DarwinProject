using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}