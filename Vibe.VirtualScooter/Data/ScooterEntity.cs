using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.VirtualScooter.Data
{
    [Table("scooters")]
    public class ScooterEntity
    {
        public Guid Id { get; set; }
        public String SerialNumber { get; set; }
        public String Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
