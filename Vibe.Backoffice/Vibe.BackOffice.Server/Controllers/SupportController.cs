using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.SupportRequests;
using Vibe.Domain.SupportRequests.SupportMessages;
using Vibe.Services.SupportRequests.Interface;
using Vibe.Tools.ControllerExtenstions;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class SupportController : Controller
    {
        private readonly ISupportRequestService _supportRequestService;
        public SupportController(ISupportRequestService supportRequestService)
        {
            _supportRequestService = supportRequestService;
        }

        [Authorize(Roles = "Client")]
        [HttpPost("SaveSupportRequest")]
        public Result SaveRequest([FromBody] SupportRequestDTO request)
        {
            return _supportRequestService.SaveSupportRequest(request, User.GetUserId());
        }

        [Authorize(Roles = "Client")]
        [HttpPost("SaveSupportMessage")]
        public Result SaveMessage([FromBody] SupportMessageDTO message)
        {
            return _supportRequestService.SaveSupportMessage(message, User.GetUserId());
        }

        [Authorize(Roles = "Client")]
        [HttpGet("GetSupportRequestDetail")]
        public SupportRequestDetail? GetSupportRequestDetail(Guid id)
        {
            return _supportRequestService.GetSupportRequestDetail(id);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("GetSupportRequests")]
        public SupportRequest[] GetSupportRequests()
        {
            return _supportRequestService.GetSupportRequests(User.GetUserId());
        }

    }
}
