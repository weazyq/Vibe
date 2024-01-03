using Vibe.Domain.Scooter;
using Vibe.Services.Scooters.Interface;

namespace Vibe.Services.Scooters
{
    public class ScootersService : IScootersService
    {
        private IScootersRepository _scooterRepository { get; init; }
        private IScootersProvider _scootersProvider { get; init; }

        public ScootersService(IScootersRepository repository, IScootersProvider scootersProvider)
        {
            _scooterRepository = repository;
            _scootersProvider = scootersProvider;
        }

        public ScooterView GetScooterView(Guid id)
        {
            return _scooterRepository.GetScooterView(id);
        }

        public Scooter GetScooter(Guid id) 
        {
            return _scooterRepository.GetScooter(id);
        }

        public async Task CheckScooterAvailability(Scooter scooter) 
        {
            await _scootersProvider.CheckScooterAvailability(scooter);
        }
    }
}
