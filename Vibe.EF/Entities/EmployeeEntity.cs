using Vibe.EF.Entities.Base;

namespace Vibe.EF.Entities
{
    public class EmployeeEntity : Auditable, IHaveId, IRemovable
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public Int32 Role { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}
