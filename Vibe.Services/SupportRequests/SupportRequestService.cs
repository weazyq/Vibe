using Vibe.Domain.Clients;
using Vibe.Domain.SupportRequests;
using Vibe.Domain.SupportRequests.SupportMessages;
using Vibe.EF.Interface;
using Vibe.Services.Clients.Interface;
using Vibe.Services.SupportRequests.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services.SupportRequests
{
    public class SupportRequestService : ISupportRequestService
    {
        private readonly IClientService _clientService;
        private readonly ISupportRequestRepository _supportRequestRepository;

        public SupportRequestService(IClientService clientService, ISupportRequestRepository supportRequestRepository)
        {
            _clientService = clientService;
            _supportRequestRepository = supportRequestRepository;
        }

        public Result SaveSupportRequest(SupportRequestDTO supportRequest, Guid userId)
        {
            Client? client = _clientService.GetClientByUser(userId);
            if (client is null) return Result.Fail("Клиент не существует");

            SupportRequestBlank blank = new()
            {
                Id = Guid.NewGuid(),
                Title = supportRequest.Title,
                Description = supportRequest.Description,
                ClientId = client.Id,
                OpenedAt = DateTime.UtcNow,
            };

            return _supportRequestRepository.SaveSupportRequest(blank);
        }

        public Result<Guid> SaveSupportMessage(SupportMessageDTO message, Guid userId)
        {
            Client? client = _clientService.GetClientByUser(userId);

            SupportMessageBlank blank = new()
            {
                Id = Guid.NewGuid(),
                Text = message.Message,
                CreatedBy = client.Id,
                SupportRequestId = message.SupportRequestId,
            };

            return _supportRequestRepository.SaveSupportMessage(blank);
        }

        public SupportRequestDetail? GetSupportRequestDetail(Guid id)
        {
            return _supportRequestRepository.GetSupportRequestDetail(id);
        }

        public SupportMessage? GetSupportMessage(Guid id)
        {
            return _supportRequestRepository.GetSupportMessage(id);
        }

        public SupportRequest[] GetSupportRequests(Guid userId)
        {
            Client? client = _clientService.GetClientByUser(userId);
            if (client is null) return [];

            return _supportRequestRepository.GetSupportRequests(client.Id);
        }
    }
}
