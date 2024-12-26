

using DatingApp.Models;

namespace dating_app_server.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
