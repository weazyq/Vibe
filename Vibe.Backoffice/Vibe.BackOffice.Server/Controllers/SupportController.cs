using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Vibe.Chat.Hubs;
using Vibe.Domain.SupportRequests;
using Vibe.Domain.SupportRequests.SupportMessages;
using Vibe.Services.SupportRequests.Interface;
using Vibe.Tools.ControllerExtenstions;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class SupportController : Controller
    {
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;

        private readonly ISupportRequestService _supportRequestService;

        public SupportController(IHubContext<ChatHub, IChatClient> hubContext, ISupportRequestService supportRequestService)
        {
            _hubContext = hubContext;

            _supportRequestService = supportRequestService;
        }

        [Authorize(Roles = "Client")]
        [HttpPost("SaveSupportRequest")]
        public Result SaveRequest([FromBody] SupportRequestDTO request)
        {
            return _supportRequestService.SaveSupportRequest(request, User.GetUserId());
        }

        [Authorize(Roles = "Client, Employee")]
        [HttpPost("SupportRequests/SaveSupportMessage")]
        public Result SaveMessage([FromBody] SupportMessageDTO message)
        {
            String role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;

            Result<Guid> saveMessageResult =  _supportRequestService.SaveSupportMessage(message, User.GetUserId(), role);
            if (saveMessageResult.IsFail) return saveMessageResult;

            Guid messageId = saveMessageResult.Value;

            SupportMessage? savedMessage = _supportRequestService.GetSupportMessage(messageId);
            if (savedMessage is null) return Result.Fail("Не удалось получить сохранённое сообщение");

            _hubContext.Clients.Group(message.SupportRequestId.ToString()).ReceiveMessage(savedMessage);

            return saveMessageResult;
        }

        [Authorize(Roles = "Client, Employee")]
        [HttpGet("SupportRequests/GetSupportRequestDetail")]
        public SupportRequestDetail? GetSupportRequestDetail(Guid id)
        {
            return _supportRequestService.GetSupportRequestDetail(id);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("SupportRequests/GetSupportRequests")]
        public SupportRequest[] GetSupportRequests()
        {
            return _supportRequestService.GetSupportRequests(User.GetUserId());
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("SupportRequests/List")]
        public SupportRequest[] ListForEmployee()
        {
            return _supportRequestService.ListSupportRequestsForEmployee(User.GetUserId());
        }
    }
}
