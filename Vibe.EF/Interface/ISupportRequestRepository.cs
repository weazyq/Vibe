using Vibe.Domain.SupportRequests;
using Vibe.Domain.SupportRequests.SupportMessages;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface ISupportRequestRepository
    {
        Result SaveSupportRequest(SupportRequestBlank blank);
        Result<Guid> SaveSupportMessage(SupportMessageBlank blank);
        SupportRequestDetail? GetSupportRequestDetail(Guid id);
        SupportMessage? GetSupportMessage(Guid id);
        SupportRequest[] GetSupportRequests(Guid clientId);
        SupportRequest[] ListSupportRequestsForEmployee(Guid employeeId);
    }
}
