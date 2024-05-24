using Vibe.Domain.SupportRequests.SupportMessages;
using Vibe.EF.Entities.SupportEntities;

namespace Vibe.Services.SupportRequests.Converters
{
    public static class SupportMessageConverter
    {
        public static SupportMessage ToDomain(this SupportMessageEntity entity)
        {
            return new SupportMessage(entity.Id, entity.Text, entity.CreatedAt, entity.ModifiedAt, entity.CreatedBy);
        }

        public static SupportMessage[] ToDomain(this SupportMessageEntity[] entities)
        {
            return entities.Select(e => e.ToDomain()).ToArray();
        }
    }
}
