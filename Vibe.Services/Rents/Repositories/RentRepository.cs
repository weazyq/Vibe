using Vibe.Domain.Rents;
using Vibe.EF;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Rents.Converters;
using Vibe.Tools.Result;

namespace Vibe.Services.Rents.Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly DataContext _context;

        public RentRepository(DataContext context) 
        {
            _context = context;
        }

        public Guid Save(Guid clientId, Guid scooterId)
        {
            RentEntity entity = new RentEntity
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                ScooterId = scooterId,
                CreatedAt = DateTime.UtcNow,
                StartedAt = DateTime.UtcNow,
                EndedAt = DateTime.UtcNow,
            };

            _context.Rents.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public Result UpdateRent(Rent rent)
        {
            RentEntity? rentEntity = _context.Rents.First(r => r.Id == rent.Id);

            rentEntity.UpdateByRent(rent);
            _context.Rents.Update(rentEntity);
            _context.SaveChanges();
            return Result.Success;
        }

        public Rent? GetRent(Guid id)
        {
            RentEntity? entity = _context.Rents.FirstOrDefault(r => r.Id == id);
            if (entity is null) return null;

            return entity.ToDomain();
        }

        public Rent? GetRentByClient(Guid clientId)
        {
            RentEntity? rent = _context.Rents.FirstOrDefault();
            if (rent is null) return null;
            return rent.ToDomain();
        }

        public Rent? GetActiveRent(Guid clientId)
        {
            RentEntity? rent = _context.Rents
                .Where(r => r.ClientId == clientId)
                .Where(r => !r.IsClosed)
                .OrderByDescending(r => r.StartedAt)
                .FirstOrDefault();

            if (rent is null) return null;
            return rent.ToDomain();
        }

        public Rent[] GetRentHistory(Guid clientId)
        {
            RentEntity[] rentEntities = _context.Rents
                .Where(r => r.ClientId == clientId)
                .Where(r => r.IsClosed)
                .OrderBy(r => r.StartedAt)
                .ToArray();

            return rentEntities.ToDomains();
        }
    }
}
