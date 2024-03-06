using Vibe.Domain.Scooter;
using Vibe.Tools.Result;

namespace Vibe.Services.Scooters.Interface
{
    public interface IScootersService
    {
        Task<Result> CheckScooterAvailability(Guid id);
        Scooter? GetScooter(Guid id);
        Task<Result> EndRent(Guid scooterId);
    }
}
