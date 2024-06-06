using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vibe.Domain.Rents;
using Vibe.Services.Rents.Interface;
using Vibe.Tools.ControllerExtenstions;
using Vibe.Tools.Result;

namespace Vibe.BackOffice.Server.Controllers
{
    public class RentController : Controller
    {
        private readonly IRentService _rentService;
        public RentController(IRentService rentService) 
        {
            _rentService = rentService; 
        }

        [HttpGet("InitializeRent")]
        [Authorize(Roles = "Client")]
        public async Task<Result<Rent>> InitializeRent(Guid scooterId, Guid clientId)
        {
            return await _rentService.Initialize(scooterId, clientId);
        }

        [HttpGet("EndRent")]
        [Authorize(Roles = "Client")]
        public async Task<Result> EndRent(Guid rentId)
        {
            return await _rentService.EndRent(rentId);
        }

        [HttpGet("GetActiveUserRent")]
        [Authorize(Roles = "Client")]
        public Result<Rent?> GetUserRent()
        {
            return _rentService.GetActiveRent(User.GetId());
        }

        [HttpGet("GetRentHistory")]
        [Authorize(Roles = "Client")]
        public Rent[] GetRentHistory()
        {
            return _rentService.GetRentHistory(User.GetId());
        }
    }
}
