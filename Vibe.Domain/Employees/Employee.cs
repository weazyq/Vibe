namespace Vibe.Domain.Employees
{
    public class Employee
    {
        public Guid Id { get; }
        public String Name { get; }
        public String Phone { get; }
        public String Login { get; }
        public String Email { get; }

        public Employee(Guid id, String name, String phone, String login, String email)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Login = login;
            Email = email;
        }
    }
}
