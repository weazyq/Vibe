using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Clients;
using Vibe.Services.Clients.Interface;
using Vibe.Services.Users.Interface;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("SendSms")]
        public Result SendSms(String phoneNumber)
        {
            return _clientService.SendSms(phoneNumber);
        }
        
        public record CheckSmsRequest(ClientBlank ClientBlank, String Code);
        [HttpPost("CheckSms")]
        public Result CheckSms([FromBody] CheckSmsRequest request)
        {
            return _clientService.CheckSms(request.ClientBlank, request.Code);
        }
    }
}
