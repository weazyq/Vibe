using Vibe.Domain.Scooter;
using Vibe.Tools.Result;

namespace Vibe.Services.Scooters.Interface
{
    public interface IScootersService
    {
        Task<Result> CheckScooterAvailability(Scooter scooter);
        Scooter? GetScooter(Guid id);
        Scooter[] GetScooters();
        ScooterView GetScooterView(Guid id);
    }
}
