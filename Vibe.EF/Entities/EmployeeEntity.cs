using Vibe.EF.Entities.Base;

namespace Vibe.EF.Entities
{
    public class EmployeeEntity : Auditable, IHaveId, IRemovable
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}
