using Vibe.Domain.Scooter;
using Vibe.Services.Scooters.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services.Scooters
{
    public class ScootersProvider : IScootersProvider
    {
        private readonly HttpClient _httpClient;

        public ScootersProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CheckScooterAvailability(Scooter scooter)
        {
            Uri url = MakeApiUrl(scooter, "CheckScooterAvailability");
            Object obj = await _httpClient.GetAsync(url);
        }

        private Uri MakeApiUrl(Scooter scooter, String action)
        {
            Uri url = new($"{scooter.Ip}:{scooter.Port}/{action}");
            return url;
        }
    }
}
