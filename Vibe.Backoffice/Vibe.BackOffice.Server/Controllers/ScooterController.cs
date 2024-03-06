using Microsoft.AspNetCore.Mvc;
using Vibe.Services.Scooters.Interface;

namespace Vibe.BackOffice.Server.Controllers
{
    public class ScooterController : Controller
    {
        private IScootersService _scootersService { get; init; }
        
        public ScooterController(IScootersService scootersService) 
        {
            _scootersService = scootersService;
        }
    }
}
