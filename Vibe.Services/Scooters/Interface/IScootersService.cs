using Vibe.Domain.Scooter;

namespace Vibe.Services.Scooters.Interface
{
    public interface IScootersService
    {
        Task CheckScooterAvailability(Scooter scooter);
        Scooter? GetScooter(Guid id);
        Scooter[] GetScooters();
        ScooterView GetScooterView(Guid id);
    }
}
