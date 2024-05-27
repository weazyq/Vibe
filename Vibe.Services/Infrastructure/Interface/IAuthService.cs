using Vibe.Domain.Employees;
using Vibe.Tools.Result;

namespace Vibe.Services.Infrastructure.Interface
{
    public interface IAuthService
    {
        String LoginEmployee(Employee employee);
        Result<(String Token, String RefreshToken)> LoginClient(Guid userId);
    }
}
