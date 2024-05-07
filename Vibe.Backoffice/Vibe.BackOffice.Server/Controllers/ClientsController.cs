using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Clients;
using Vibe.Domain.Infrastructure;
using Vibe.Domain.Users;
using Vibe.Services.Clients.Interface;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Services.Users.Interface;
using Vibe.Tools.ControllerExtenstions;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public ClientsController(IClientService clientService, IUserService userService, IAuthService authService)
        {
            _clientService = clientService;
            _userService = userService;
            _authService = authService;
        }

        [Authorize(Roles = "Client")]
        [HttpGet("GetClient")]
        public Client GetClientByUser()
        {
            return _clientService.GetClientByUser(User.GetUserId());
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
        public Result<LoginResultDTO?> CheckSms([FromBody] CheckSmsRequest request)
        {
            Result checkSmsResult = _clientService.CheckSms(request.ClientBlank, request.Code);
            if(checkSmsResult.IsFail) return checkSmsResult;

            Result<Guid> saveClientResult = _clientService.SaveClient(request.ClientBlank);
            if (saveClientResult.IsFail) return new Result<LoginResultDTO?>(null, saveClientResult.Error);

            Result<Guid> saveUserResult = _userService.SaveUserByClient(saveClientResult.Value);
            if (saveUserResult.IsFail) return new Result<LoginResultDTO?>(null, saveClientResult.Error);

            Result<(String Token, String RefreshToken)> loginResult = _authService.Login(saveUserResult.Value);
            if (loginResult.IsFail) return new Result<LoginResultDTO?>(null, loginResult.Error);

            return new LoginResultDTO(saveUserResult.Data, loginResult.Data.Token, loginResult.Data.RefreshToken);
        }

        [HttpPost("Login")]
        public Result<LoginResultDTO?> Login([FromBody] CheckSmsRequest request)
        {
            Result checkSmsResult = _clientService.CheckSms(request.ClientBlank, request.Code);
            if (checkSmsResult.IsFail) return checkSmsResult;

            if (String.IsNullOrWhiteSpace(request.ClientBlank.Phone)) return Result.Fail("Не указан номер телефона клиента");
            Client? client = _clientService.GetClientByPhoneNumber(request.ClientBlank.Phone);
            if (client is null) return Result.Fail("Клиент не существует в системе");

            User? user = _userService.GetUserByClientId(client.Id);
            if (user is null) return Result.Fail("Пользователь не существует в системе");

            Result<(String Token, String RefreshToken)> loginResult = _authService.Login(user.Id);
            if (loginResult.IsFail) return new Result<LoginResultDTO?>(null, loginResult.Error);

            return new LoginResultDTO(user.Id, loginResult.Data.Token, loginResult.Data.RefreshToken);
        }
    }
}
