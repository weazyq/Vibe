using Vibe.Domain.Scooter;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Scooters.Converters;
using Vibe.Services.Scooters.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services.Scooters
{
    public class ScootersService : IScootersService
    {
        private IDataRepository<ScooterEntity> _scooterRepository { get; init; }
        private IScootersProvider _scootersProvider { get; init; }

        public ScootersService(IDataRepository<ScooterEntity> repository, IScootersProvider scootersProvider)
        {
            _scooterRepository = repository;
            _scootersProvider = scootersProvider;
        }

        public Scooter? GetScooter(Guid id)
        {
            ScooterEntity? entity = _scooterRepository.Get(id);
            if (entity == null) return null;

            return entity.ToDomain();
        }

        public Scooter[] GetScooters()
        {
            return _scooterRepository.List().ToDomain();
        }

        public async Task<Result> CheckScooterAvailability(Scooter scooter) 
        {
            return await _scootersProvider.CheckScooterAvailability(scooter);
        }

        public ScooterView GetScooterView(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
