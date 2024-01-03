namespace Vibe.EF.Entities
{
    public class UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? ClientId { get; set; }
    }
}
