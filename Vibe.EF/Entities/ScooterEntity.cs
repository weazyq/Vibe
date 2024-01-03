using Vibe.EF.Entities.Base;

namespace Vibe.EF.Entities
{
    public class ScooterEntity : Auditable, IHaveId, IRemovable
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Ip { get; set; }
        public String Port { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}
