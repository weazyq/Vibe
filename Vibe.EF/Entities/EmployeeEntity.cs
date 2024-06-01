namespace Vibe.EF.Entities
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }
        public required String Name { get; set; }
        public required String Phone { get; set; }
        public required String Email { get; set; }
        public required String Login { get; set; }
        public required String Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}
