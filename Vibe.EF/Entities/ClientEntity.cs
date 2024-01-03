using Vibe.EF.Entities.Base;

namespace Vibe.EF.Entities
{
    public class ClientEntity : Auditable, IHaveId, IRemovable
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}
