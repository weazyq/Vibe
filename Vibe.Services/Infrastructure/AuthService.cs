﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Vibe.Configurator.Configuration;
using Vibe.Domain.Users;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Tools.JWT;
using Vibe.Tools;
using Vibe.Tools.Result;
using Vibe.Tools.Token;
using Vibe.Services.Users.Interface;

namespace Vibe.Services.Infrastructure
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserService userService, IConfiguration configuration) 
        {
            _userService = userService;
            _configuration = configuration;
        }

        public Result<(String Token, String RefreshToken)> Login(Guid userId)
        {
                User? user = _userService.GetUser(userId);
                if (user is null) return Result.Fail("Не удалось авторизовать пользователя. Пользователь не существует в системе");

                String token = CreateToken(user);
                RefreshToken newRefreshToken = GenerateRefreshToken();
                SetRefreshToken(user, newRefreshToken);
                return (token, newRefreshToken.Token);
        }

        private String CreateToken(User user)
        {
            String role = user.ClientId != null
                ? "Client"
                : "Employee";

            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            ];

            //срок действия токена
            DateTime startDateTime = DateTime.UtcNow;
            DateTime expireDateTime = DateTime.UtcNow.AddDays(1);


            String secretKey = CustomConfigurationExtensions.GetRequiredStringValue(_configuration, nameof(JWTSettings), nameof(JWTSettings.SecretKey));
            String issuer = CustomConfigurationExtensions.GetRequiredStringValue(_configuration, nameof(JWTSettings), nameof(JWTSettings.Issuer));
            String audience = CustomConfigurationExtensions.GetRequiredStringValue(_configuration, nameof(JWTSettings), nameof(JWTSettings.Audience));

            SymmetricSecurityKey signingKey = JWTTools.FormSingingKey(secretKey);
            SigningCredentials credentials = new(signingKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(issuer, audience, claims, startDateTime, expireDateTime, credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetRefreshToken(User user, RefreshToken newRefreshToken)
        {
            user.SetRefreshToken(newRefreshToken);
            _userService.UpdateUser(user);
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            return refreshToken;
        }
    }
}
