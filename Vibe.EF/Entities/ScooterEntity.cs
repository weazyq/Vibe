using Vibe.EF.Entities.Base;

namespace Vibe.EF.Entities
{
    public class ScooterEntity : Auditable, IHaveId
    {
        public Guid Id { get; set; }
        public String Url { get; set; }
        public String SerialNumber { get; set; }
    }
}
