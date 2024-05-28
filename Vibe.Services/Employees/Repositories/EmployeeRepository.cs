using Vibe.Domain.Employees;
using Vibe.EF;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Employees.Converters;
using Vibe.Tools.Result;

namespace Vibe.Services.Employees.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        
        public Result SaveEmployee(EmployeeBlank blank)
        {
            EmployeeEntity employee = new EmployeeEntity()
            {
                Id = blank.Id,
                Login = blank.Login,
                Password = blank.Password,
                Phone = blank.Phone,
                Name = blank.Name,
                Email = blank.Email,
                CreatedAt = DateTime.UtcNow,
            };

            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Result.Fail($"Не удалось сохранить сотрудника {e.Message}");
            }

            return Result.Success;
        }

        public Employee? GetEmployee(Guid id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id)?.ToDomain();
        }

        public Employee? GetEmployee(String login)
        {
            return _context.Employees.FirstOrDefault(e => e.Login == login)?.ToDomain();
        }

        public Employee[] List()
        {
            return _context.Employees.Where(e => !e.IsRemoved).ToArray().ToDomain();
        }

        public Boolean CheckIsPasswordEquals(Guid employeeId, String password)
        {
            EmployeeEntity employee = _context.Employees.First(e => e.Id == employeeId);
            if (employee.Password != password) return false;

            return true;
        }

        public Result RemoveEmployee(Guid employeeId)
        {
            EmployeeEntity? existEmployee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (existEmployee is null) return Result.Fail("Сотрудник не найден");

            existEmployee.IsRemoved = true;

            try
            {
                _context.Employees.Update(existEmployee);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Result.Fail($"Не удалось удалить сотрудника. {e}");
            }

            return Result.Success;
        }
    }
}
