using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Scooter;
using Vibe.Services.Scooters.Interface;
using Vibe.Tools.Result;

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
