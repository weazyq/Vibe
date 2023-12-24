using Microsoft.AspNetCore.Mvc;
using Vibe.EF;
using Vibe.EF.Entities;

namespace Vibe.Backoffice.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private DataContext _dataContext { get; init; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet("/Test")]
        public User[] GetClients()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Login = "weazy",
                Password = "123",
                CreatedAt = DateTime.Now
            };

            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();

            return _dataContext.Users.ToArray();
        }
    }
}
