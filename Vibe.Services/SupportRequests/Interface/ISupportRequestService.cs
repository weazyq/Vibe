using Vibe.Domain.SupportRequests;
using Vibe.Domain.SupportRequests.SupportMessages;
using Vibe.Tools.Result;

namespace Vibe.Services.SupportRequests.Interface
{
    public interface ISupportRequestService
    {
        Result SaveSupportRequest(SupportRequestDTO supportRequest, Guid userId);
        Result SaveSupportMessage(SupportMessageDTO message, Guid userId);
        SupportRequestDetail? GetSupportRequestDetail(Guid id);
        SupportRequest[] GetSupportRequests(Guid userId);
    }
}
