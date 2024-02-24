namespace Vibe.Domain.Rents
{
    public class Rent
    {
        public Guid Id;
        public Guid ClientId;
        public Guid ScooterId;
        public Decimal? Price;
        public Boolean IsClosed;
        public DateTime StartedAt;
        public DateTime? EndedAt;
        public DateTime CreatedAt;
        public DateTime? ModifiedAt;

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
