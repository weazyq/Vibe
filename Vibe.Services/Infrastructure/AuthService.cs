using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Vibe.Configurator.Configuration;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Tools.JWT;
using Vibe.Tools;
using Vibe.Tools.Result;
using Vibe.Tools.Token;
using Vibe.Domain.Employees;
using Vibe.Domain.Clients;
using Vibe.Services.Clients.Interface;

namespace Vibe.Services.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IClientService _clientService;
    private readonly IConfiguration _configuration;

    public AuthService(IClientService clientService, IConfiguration configuration) 
    {
        _clientService = clientService;
        _configuration = configuration;
    }

    public String LoginEmployee(Employee employee)
    {
        return CreateToken(employee.Id, "Employee");
    }

    public Result<(String Token, String RefreshToken)> LoginClient(Guid clientId)
    {
        Client? client = _clientService.GetClient(clientId);
        if (client is null) return Result.Fail("Не удалось авторизовать клиента. Клиент не существует в системе");

        String token = CreateToken(client.Id, "Client");
        RefreshToken newRefreshToken = GenerateRefreshToken();
        SetRefreshToken(client, newRefreshToken);
        return (token, newRefreshToken.Token);
    }

    private String CreateToken(Guid id, String role)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
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

    private void SetRefreshToken(Client client, RefreshToken newRefreshToken)
    {
        client.SetRefreshToken(newRefreshToken);
        _clientService.UpdateClient(client);
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
