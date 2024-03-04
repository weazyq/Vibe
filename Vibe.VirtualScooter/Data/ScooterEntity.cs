using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.VirtualScooter.Data
{
    [Table("scooters")]
    public class ScooterEntity
    {
        public Guid Id { get; set; }
        public String SerialNumber { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
