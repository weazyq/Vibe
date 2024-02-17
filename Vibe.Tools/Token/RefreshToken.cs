namespace Vibe.Tools.Token
{
    public class RefreshToken
    {
        public String Token { get; set; } = String.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
