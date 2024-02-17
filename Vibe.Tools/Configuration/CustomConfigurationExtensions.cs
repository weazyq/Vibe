using Microsoft.Extensions.Configuration;

namespace Vibe.Tools;

public static class CustomConfigurationExtensions
{
    public static String GetRequiredStringValue(this IConfiguration configuration, String section, String key)
    {
        String? value = configuration.GetSection($"{section}:{key}").Value;
        if (value == null) throw new InvalidOperationException($"В appsettings не указан {key} для раздела {section}");

        return value;
    }
}
