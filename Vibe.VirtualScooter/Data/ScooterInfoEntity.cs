using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Vibe.Domain.Scooter;

namespace Vibe.VirtualScooter.Data
{
    [Table("scooterinfo")]
    [PrimaryKey(nameof(ScooterId))]
    public class ScooterInfoEntity
    {
        public Guid ScooterId { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public Double Charge {  get; set; }
        public ScooterState State { get;set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
