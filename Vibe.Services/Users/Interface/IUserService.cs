using Vibe.Domain.Users;
using Vibe.Tools.Result;

namespace Vibe.Services.Users.Interface
{
    public interface IUserService
    {
        User? GetUser(Guid userId);
        User? GetUserByRefreshToken(String refreshToken);
        Result<Guid> SaveUserByClient(Guid clientId);
        Result UpdateUser(User user);
    }
}
