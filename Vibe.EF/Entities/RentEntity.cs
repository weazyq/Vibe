using Vibe.Domain.Rents;

namespace Vibe.EF.Entities
{
    public class RentEntity
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ScooterId { get; set; }
        public Decimal? Price { get; set; }
        public Boolean IsClosed { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public void UpdateByRent(Rent rent)
        {
            Price = rent.Price;
            EndedAt = rent.EndedAt;
            ModifiedAt = rent.ModifiedAt;
            IsClosed = rent.IsClosed;
        }
    }
}
