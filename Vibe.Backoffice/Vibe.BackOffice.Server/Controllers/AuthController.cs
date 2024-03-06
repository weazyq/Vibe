using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Infrastructure;
using Vibe.Domain.Users;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Services.Users.Interface;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("Auth/RefreshToken")]
        public Result<LoginResultDTO> RefreshToken(String? refreshToken)
        {
            if (refreshToken is null) return Result.Fail("");

            User? user = _userService.GetUserByRefreshToken(refreshToken);
            if (user is null) return Result.Fail("");

            if (user.TokenExpires < DateTime.Now) return Result.Fail("");

            Result<(String Token, String RefreshToken)> loginResult = _authService.Login(user.Id);

            return new LoginResultDTO(user.Id, loginResult.Data.Token, loginResult.Data.RefreshToken);
        } 
    }
}
