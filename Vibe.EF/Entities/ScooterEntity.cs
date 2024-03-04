using Vibe.EF.Entities.Base;

namespace Vibe.EF.Entities
{
    public class ScooterEntity : IHaveId
    {
        public Guid Id { get; set; }
        public String SerialNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
