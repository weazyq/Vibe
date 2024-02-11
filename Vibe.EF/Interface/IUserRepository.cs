using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IUserRepository
    {
        Result SaveUserByClient(Guid clientId);
    }
}
