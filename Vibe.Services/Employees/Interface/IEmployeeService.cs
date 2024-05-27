using Vibe.Domain.Employees;
using Vibe.Domain.Infrastructure;
using Vibe.Tools.Result;

namespace Vibe.Services.Employees.Interface;

public interface IEmployeeService
{
    Result SaveEmployee(EmployeeBlank blank);

    Employee? GetEmployee(Guid id);
    Employee? GetEmployee(String login);
    Result<EmployeeLoginResultDTO> Login(String login, String password);
}
