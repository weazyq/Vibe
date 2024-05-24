namespace Vibe.Domain.SupportRequests.SupportMessages
{
    public class SupportMessageBlank
    {
        public Guid Id { get; set; }
        public String Text { get; set; }
        public Guid SupportRequestId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
