using System.ComponentModel.DataAnnotations.Schema;
using Vibe.Domain.Scooter;

namespace Vibe.VirtualScooter.Data
{
    [Table("scooters")]
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

        public void Update(ScooterEntity entity)
        {
            Latitude = entity.Latitude;
            Longitude = entity.Longitude;
            Charge = entity.Charge;
            State = entity.State;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
