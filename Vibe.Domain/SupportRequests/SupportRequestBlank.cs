namespace Vibe.Domain.SupportRequests
{
    public class SupportRequestBlank
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Guid ClientId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime OpenedAt { get; set; }
    }
}
