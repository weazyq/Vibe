using Vibe.Domain.Scooter;
using Vibe.Tools.Result;

namespace Vibe.Services.Scooters.Interface
{
    public interface IScootersProvider
    {
        public Task<Result> CheckScooterAvailability(Scooter scooter);
    }
}
