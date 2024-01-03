namespace Vibe.EF.Entities
{
    public class WorkzoneEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public String Name { get; set; }
    }
}
