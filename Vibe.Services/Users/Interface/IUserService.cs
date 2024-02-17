using Vibe.Domain.Users;
using Vibe.Tools.Result;

namespace Vibe.Services.Users.Interface
{
    public interface IUserService
    {
        User? GetUser(Guid userId);
        Result<Guid> SaveUserByClient(Guid clientId);
        Result<(String Token, String RefreshToken)> Login(Guid userId);
    }
}
