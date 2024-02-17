namespace Vibe.BackOffice.Server.Tools
{
    public class JWTSettings
    {
        public String SecretKey { get; set; } = String.Empty;
        public String Issuer { get; set; } = String.Empty;
        public String Audience { get; set; } = String.Empty;
    }
}
