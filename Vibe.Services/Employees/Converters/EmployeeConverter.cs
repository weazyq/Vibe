using Vibe.Domain.Employees;
using Vibe.EF.Entities;

namespace Vibe.Services.Employees.Converters
{
    public static class EmployeeConverter
    {
        public static Employee ToDomain(this EmployeeEntity entity)
        {
            return new Employee(entity.Id, entity.Name, entity.Phone, entity.Login, entity.Email);
        }

        public static Employee[] ToDomain(this EmployeeEntity[] entities)
        {
            return entities.Select(e => e.ToDomain()).ToArray();
        }
    }
}
