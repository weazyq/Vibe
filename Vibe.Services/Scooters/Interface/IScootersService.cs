using Vibe.Domain.Scooter;
using Vibe.Tools.Result;

namespace Vibe.Services.Scooters.Interface
{
    public interface IScootersService
    {
        Task<Result> CheckScooterAvailability(Guid id);
        Scooter? GetScooter(Guid id);
        Scooter[] GetScooters();
        Task<Result> EndRent(Guid scooterId);
    }
}
