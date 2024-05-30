using Vibe.Domain.Clients;
using Vibe.Domain.Employees;
using Vibe.Domain.SupportRequests.SupportMessages;

namespace Vibe.Domain.SupportRequests
{
    public class SupportRequestDetail
    {
        public Guid Id { get; }
        public String Title { get; }
        public String Description { get; }
        public Client Client { get; }
        public Employee? Employee { get; }
        public DateTime OpenedAt { get; set; }
        public Boolean IsClosed { get; }
        public SupportMessage[] Messages { get; }

        public SupportRequestDetail(Guid id, String title, String description, Client client, Employee? employee,
            DateTime openedAt, Boolean isClosed, SupportMessage[] messages)
        {
            Id = id;
            Title = title;
            Description = description;
            Client = client;
            Employee = employee;
            OpenedAt = openedAt;
            IsClosed = isClosed;
            Messages = messages;
        }
    }
}
