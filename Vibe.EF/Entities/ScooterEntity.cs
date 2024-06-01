using Vibe.Domain.Scooter;

namespace Vibe.EF.Entities
{
    public class ScooterEntity
    {
        public Guid Id { get; set; }
        public String? SerialNumber { get; set; } = String.Empty;
        public Double? Latitude { get; set; }
        public Double? Longitude { get; set; }
        public Double? Charge { get; set; }
        public ScooterState? State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
