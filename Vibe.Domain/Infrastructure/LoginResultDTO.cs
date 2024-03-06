namespace Vibe.Domain.Infrastructure
{
    public class LoginResultDTO
    {
        public Guid UserId { get; set; }
        public String Token { get; set; } = String.Empty;
        public String RefreshToken { get; set; } = String.Empty;

        public LoginResultDTO(Guid userId, String token, String refreshToken)
        {
            UserId = userId;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
