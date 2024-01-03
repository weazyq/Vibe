using Vibe.Domain.Scooter;
using Vibe.Services.Scooters.Interface;

namespace Vibe.Services.Scooters.Repository
{
    public class ScootersRepository : IScootersRepository
    {
        public Scooter GetScooter(Guid id)
        {
            return new Scooter(Guid.NewGuid(), "", "", "");
        }

        public ScooterView GetScooterView(Guid id)
        {
            return new ScooterView(Guid.NewGuid(), "", 1, 15, 100, ScooterState.AvailableForRent);
        }
    }
}