using Vibe.Domain.Employees;
using Vibe.Domain.Infrastructure;
using Vibe.EF.Interface;
using Vibe.Services.Employees.Interface;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAuthService _authService;
        
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IAuthService authService) 
        {
            _authService = authService;

            _employeeRepository = employeeRepository;
        }

        public Result SaveEmployee(EmployeeBlank blank)
        {
            return _employeeRepository.SaveEmployee(blank);
        }

        public Employee? GetEmployee(Guid id)
        {
            return _employeeRepository.GetEmployee(id);
        }

        public Employee? GetEmployee(String login)
        {
            return _employeeRepository.GetEmployee(login);
        }

        public Result<EmployeeLoginResultDTO> Login(String login, String password)
        {
            Employee? existEmployee = GetEmployee(login);
            if (existEmployee is null) return Result.Fail($"Пользователь с логином {login} не существует");

            Boolean isPasswordEquals = _employeeRepository.CheckIsPasswordEquals(existEmployee.Id, password);
            if (!isPasswordEquals) return Result.Fail("Введённый пароль не совпадает с паролем пользователя");

            String token = _authService.LoginEmployee(existEmployee);
            return new EmployeeLoginResultDTO(existEmployee.Id, token);
        }
    }
}
