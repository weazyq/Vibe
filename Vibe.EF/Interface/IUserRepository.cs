using Vibe.Domain.Users;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IUserRepository
    {
        User? GetUser(Guid userId);
        Result<Guid> SaveUserByClient(Guid clientId);
        Result UpdateUser(User user);
    }
}
