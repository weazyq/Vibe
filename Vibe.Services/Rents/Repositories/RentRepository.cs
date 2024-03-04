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
            try
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
            catch (Exception e)
            {
                throw;
            }
        }

        public Result UpdateRent(Rent rent)
        {
            RentEntity? rentEntity = _context.Rents.FirstOrDefault(r => r.Id == rent.Id);
            if (rentEntity == null) return Result.Fail("");

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
    }
}
