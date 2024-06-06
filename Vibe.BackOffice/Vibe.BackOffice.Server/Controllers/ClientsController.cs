using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Clients;
using Vibe.Domain.Infrastructure;
using Vibe.Services.Clients.Interface;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Tools.ControllerExtenstions;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IAuthService _authService;

        public ClientsController(IClientService clientService, IAuthService authService)
        {
            _clientService = clientService;
            _authService = authService;
        }

        [Authorize(Roles = "Client")]
        [HttpGet("GetClient")]
        public Client? GetClientByUser()
        {
            return _clientService.GetClient(User.GetId());
        }

        [HttpGet("CheckPhoneNumber")]
        public Boolean CheckPhoneNumber(String phoneNumber)
        {
            return _clientService.CheckIsPhoneNumberExist(phoneNumber);
        }

        [HttpGet("SendSms")]
        public Result SendSms(String phoneNumber)
        {
            return _clientService.SendSms(phoneNumber);
        }
        
        public record CheckSmsRequest(ClientBlank ClientBlank, String Code);
        [HttpPost("CheckSms")]
        public Result<ClientLoginResultDTO?> CheckSms([FromBody] CheckSmsRequest request)
        {
            Result checkSmsResult = _clientService.CheckSms(request.ClientBlank, request.Code);
            if(checkSmsResult.IsFail) return checkSmsResult;

            Result<Guid> saveClientResult = _clientService.SaveClient(request.ClientBlank);
            if (saveClientResult.IsFail) return new Result<ClientLoginResultDTO?>(null, saveClientResult.Error);

            Result<(String Token, String RefreshToken)> loginResult = _authService.LoginClient(saveClientResult.Value);
            if (loginResult.IsFail) return new Result<ClientLoginResultDTO?>(null, loginResult.Error);

            return new ClientLoginResultDTO(saveClientResult.Data, loginResult.Data.Token, loginResult.Data.RefreshToken);
        }

        [HttpPost("Login")]
        public Result<ClientLoginResultDTO?> Login([FromBody] CheckSmsRequest request)
        {
            Result checkSmsResult = _clientService.CheckSms(request.ClientBlank, request.Code);
            if (checkSmsResult.IsFail) return checkSmsResult;

            if (String.IsNullOrWhiteSpace(request.ClientBlank.Phone)) return Result.Fail("Не указан номер телефона клиента");
            Client? client = _clientService.GetClientByPhoneNumber(request.ClientBlank.Phone);
            if (client is null) return Result.Fail("Клиент не существует в системе");

            Result<(String Token, String RefreshToken)> loginResult = _authService.LoginClient(client.Id);
            if (loginResult.IsFail) return new Result<ClientLoginResultDTO?>(null, loginResult.Error);

            return new ClientLoginResultDTO(client.Id, loginResult.Data.Token, loginResult.Data.RefreshToken);
        }
    }
}
