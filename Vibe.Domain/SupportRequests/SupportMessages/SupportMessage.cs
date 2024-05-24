namespace Vibe.Domain.SupportRequests.SupportMessages
{
    public class SupportMessage
    {
        public Guid Id { get; }
        public String Text { get; }
        public DateTime CreatedAt { get; }
        public DateTime? ModifiedAt { get; }
        public Guid CreatedBy { get; }

        public SupportMessage(Guid id, string text, DateTime createdAt, DateTime? modifiedAt, Guid createdBy)
        {
            Id = id;
            Text = text;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            CreatedBy = createdBy;
        }
    }
}
