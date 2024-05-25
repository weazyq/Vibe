using Vibe.Domain.SupportRequests.SupportMessages;

namespace Vibe.Domain.SupportRequests
{
    public class SupportRequestDetail : SupportRequest
    {
        public SupportMessage[] Messages { get; }

        public SupportRequestDetail(Guid id, String title, String description, Guid clientId, Guid? employeeId,
            DateTime openedAt, Boolean isClosed, SupportMessage[] messages) : base(id, title, description, clientId, employeeId, openedAt, isClosed) 
        {
            Messages = messages;
        }

        public SupportRequestDetail(SupportRequest request, SupportMessage[] messages) : base(request.Id, request.Title, request.Description, request.ClientId, request.EmployeeId, request.OpenedAt, request.IsClosed)
        {
            Messages = messages;
        }
    }
}
