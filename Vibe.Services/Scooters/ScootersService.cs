using Vibe.Domain.Scooter;
using Vibe.EF.Interface;
using Vibe.Services.Scooters.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services.Scooters
{
    public class ScootersService : IScootersService
    {
        private IScooterRepository _scooterRepository { get; init; }
        private IScootersProvider _scootersProvider { get; init; }

        public ScootersService(IScooterRepository repository, IScootersProvider scootersProvider)
        {
            _scooterRepository = repository;
            _scootersProvider = scootersProvider;
        }

        public Scooter? GetScooter(Guid id)
        {
            return _scooterRepository.GetScooter(id);
        }

        public Scooter[] GetScooters()
        {
            return _scooterRepository.GetScooters();
        }

        public async Task<Result> CheckScooterAvailability(Guid scooterId) 
        {
            Scooter? scooter = GetScooter(scooterId);
            if (scooter is null) return Result.Fail("Указанный самокат не найден в системе");

            return await _scootersProvider.CheckScooterAvailability(scooter);
        }

        public async Task<Result> EndRent(Guid scooterId)
        {
            Scooter? scooter = GetScooter(scooterId);
            if (scooter is null) return Result.Fail("Указанный самокат не найден в системе");

            return await _scootersProvider.EndRent(scooter);
        }
    }
}
