using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            Devices = new List<Device>();
        }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}