using System.Text.Json;
using Vibe.Configurator.Configuration;
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

        public async Task<Result> CheckScooterAvailability(Scooter scooter)
        {
            Uri url = MakeApiUrl(scooter.SerialNumber, "CheckScooterAvailability");
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                Result? result = JsonSerializer.Deserialize<Result>(content);
                if (result != null) return result;
            }
            catch (Exception e)
            {
                return Result.Fail($"Не удалось получить информацию о состоянии самоката. Ошибка: {e.Message}");
            }
            
            return Result.Fail("Не удалось получить информацию о состоянии самоката");
        }

        public async Task<Result> EndRent(Scooter scooter)
        {
            Uri url = MakeApiUrl(scooter.SerialNumber, "EndRent");

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                Result? result = JsonSerializer.Deserialize<Result>(content);
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private Uri MakeApiUrl(String serialNumber, String action)
        {
            String host = VirtualScooterSettings.Host;
            host = host.Replace("{SerialNumber}", serialNumber);

            Uri url = new($"{host}/{action}");
            return url;
        }
    }
}
