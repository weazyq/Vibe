namespace Vibe.Domain.Employees
{
    public class EmployeeBlank
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }

        public EmployeeBlank(Guid id, String name, String phone, String login, String password, String email)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Login = login;
            Password = password;
            Email = email;
        }
    }
}
