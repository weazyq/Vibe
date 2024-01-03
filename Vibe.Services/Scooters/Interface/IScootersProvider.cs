using Vibe.Domain.Scooter;

namespace Vibe.Services.Scooters.Interface
{
    public interface IScootersProvider
    {
        public Task CheckScooterAvailability(Scooter scooter);
    }
}
