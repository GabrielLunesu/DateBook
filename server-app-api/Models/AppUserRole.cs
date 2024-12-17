using DatingApp.Models;
using Microsoft.AspNetCore.Identity;

namespace dating_app_server.Models
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser User { get; set; } = null!;
        public AppRole Role { get; set; } = null!;
    }
}
