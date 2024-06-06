using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Clients;
using Vibe.Domain.Infrastructure;
using Vibe.Services.Clients.Interface;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class AuthController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IAuthService _authService;

        public AuthController(IClientService clientService, IAuthService authService)
        {
            _clientService = clientService;
            _authService = authService;
        }

        [HttpGet("Auth/RefreshToken")]
        public Result<ClientLoginResultDTO> RefreshToken(String? refreshToken)
        {
            if (refreshToken is null) return Result.Fail("");

            Client? client = _clientService.GetClientByRefreshToken(refreshToken);
            if (client is null) return Result.Fail("Указанного клиента не существует");

            if (client.TokenExpires < DateTime.Now) return Result.Fail("");

            Result<(String Token, String RefreshToken)> loginResult = _authService.LoginClient(client.Id);

            return new ClientLoginResultDTO(client.Id, loginResult.Data.Token, loginResult.Data.RefreshToken);
        } 
    }
}
