using Microsoft.AspNetCore.Identity;

namespace dating_app_server.Models
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}
