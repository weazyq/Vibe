using Vibe.Domain.SupportRequests;
using Vibe.Domain.SupportRequests.SupportMessages;
using Vibe.Tools.Result;

namespace Vibe.Services.SupportRequests.Interface
{
    public interface ISupportRequestService
    {
        Result SaveSupportRequest(SupportRequestDTO supportRequest, Guid userId);
        Result<Guid> SaveSupportMessage(SupportMessageDTO message, Guid userId, String role);
        SupportRequestDetail? GetSupportRequestDetail(Guid id);
        SupportMessage? GetSupportMessage(Guid id);
        SupportRequest[] GetSupportRequests(Guid userId);
        SupportRequest[] ListSupportRequestsForEmployee(Guid employeeId);
    }
}
