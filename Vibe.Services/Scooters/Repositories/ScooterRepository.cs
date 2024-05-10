using Vibe.Domain.Scooter;
using Vibe.EF;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Scooters.Converters;

namespace Vibe.Services.Scooters.Repositories
{
    public class ScooterRepository : IScooterRepository
    {
        private DataContext _context { get; init; }

        public ScooterRepository(DataContext context)
        {
            _context = context;
        }

        public Scooter? GetScooter(Guid id)
        {
            ScooterEntity? scooterEntity = _context.Scooters.FirstOrDefault(s => s.Id == id);
            if (scooterEntity == null) return null;

            return scooterEntity.ToDomain();
        }

        public Scooter[] GetScooters()
        {
            ScooterEntity[] scooterEntities = _context.Scooters.ToArray();
            return scooterEntities.ToDomain();
        }
    }
}
