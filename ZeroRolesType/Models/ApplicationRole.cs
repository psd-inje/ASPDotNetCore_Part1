using Microsoft.AspNetCore.Identity;

namespace ZeroRolesType.Models
{
    public class ApplicationRole : IdentityRole
    { 
        public string Description { get; set; }
    }
}
