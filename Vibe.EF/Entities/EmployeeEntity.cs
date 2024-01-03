namespace Vibe.EF.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public Int32 Role { get; set; }
    }
}
