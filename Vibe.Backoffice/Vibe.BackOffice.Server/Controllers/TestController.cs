using Microsoft.AspNetCore.Mvc;

namespace Vibe.BackOffice.Server.Controllers
{
    public class TestController : Controller
    {
        public TestController() { }

        public record Forecast(String Date, Decimal TemperatureC, Decimal TemperatureF, String Summary);
        [HttpGet("weatherforecast")]
        public Forecast[] Get()
        {
            return [new Forecast("24-04-2024", 20, 40, "500")];
        }
    }
}
