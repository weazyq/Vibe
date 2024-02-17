using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Clients;
using Vibe.Domain.Users;
using Vibe.Services.Clients.Interface;
using Vibe.Services.Users.Interface;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;

        public ClientsController(IClientService clientService, IUserService userService)
        {
            _clientService = clientService;
            _userService = userService;
        }

        [HttpGet("SendSms")]
        public Result SendSms(String phoneNumber)
        {
            return _clientService.SendSms(phoneNumber);
        }
        
        public record CheckSmsRequest(ClientBlank ClientBlank, String Code);
        [HttpPost("CheckSms")]
        public Result<String> CheckSms([FromBody] CheckSmsRequest request)
        {
            Result checkSmsResult = _clientService.CheckSms(request.ClientBlank, request.Code);
            if(checkSmsResult.IsFail) return checkSmsResult;

            Result<Guid> saveClientResult = _clientService.SaveClient(request.ClientBlank);
            if (saveClientResult.IsFail) return new Result<String>("", saveClientResult.Error);

            Result<Guid> saveUserResult = _userService.SaveUserByClient(saveClientResult.Value);
            if (saveUserResult.IsFail) return new Result<String>("", saveClientResult.Error);

            return Result.Success;
        }
    }
}
