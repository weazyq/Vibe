namespace Vibe.Domain.Rents
{
    public class Rent
    {
        public Guid Id { get; }
        public Guid ClientId { get; private set; }
        public Guid ScooterId { get; private set; }
        public Decimal? Price { get; private set; }
        public Boolean IsClosed { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }

        public Rent(Guid id, Guid clientId, Guid scooterId, Decimal? price, Boolean isClosed, DateTime startedAt, DateTime? endedAt, DateTime createdAt, DateTime? modifiedAt)
        {
            Id = id;
            ClientId = clientId;
            ScooterId = scooterId;
            Price = price;
            IsClosed = isClosed;
            StartedAt = startedAt;
            EndedAt = endedAt;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }

        public void EndRent()
        {
            EndedAt = DateTime.UtcNow;
            Price = (EndedAt.Value.Minute - StartedAt.Minute) * 5;
            ModifiedAt = DateTime.UtcNow;
            IsClosed = true;
        }
    }
}
