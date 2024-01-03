using Vibe.EF.Entities.Base;

namespace Vibe.EF.Entities
{
    public class WorkzoneEntity : Auditable, IHaveId, IRemovable
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public String Name { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}
