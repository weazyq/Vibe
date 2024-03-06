using Vibe.Tools.Result;

namespace Vibe.Services.Infrastructure.Interface
{
    public interface IAuthService
    {
        Result<(String Token, String RefreshToken)> Login(Guid userId);
    }
}
