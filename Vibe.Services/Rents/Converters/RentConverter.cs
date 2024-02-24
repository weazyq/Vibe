using Vibe.Domain.Rents;
using Vibe.EF.Entities;

namespace Vibe.Services.Rents.Converters
{
    public static class RentConverter
    {
        public static Rent ToDomain(this RentEntity entity)
        {
            return new Rent(entity.Id, entity.ClientId, entity.ScooterId, entity.Price, entity.IsClosed, entity.StartedAt, entity.EndedAt, entity.CreatedAt, entity.ModifiedAt);
        }
    }
}
