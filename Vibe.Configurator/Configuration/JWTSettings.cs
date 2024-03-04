namespace Vibe.Configurator.Configuration;

public static class JWTSettings
{
    public static String SecretKey { get; set; } = String.Empty;
    public static String Issuer { get; set; } = String.Empty;
    public static String Audience { get; set; } = String.Empty;
}
