using Vibe.Domain.Clients;
using Vibe.EF.Entities;

namespace Vibe.Services.Clients.Converters
{
    public static class ClientConverter
    {
        public static Client ToDomain(this ClientEntity entity)
        {
            return new Client(entity.Id, entity.Name, entity.Phone, entity.CreatedAt, entity.CreatedBy, entity.ModifiedAt, entity.UpdatedBy, entity.IsRemoved);
        }
    }
}
