using Vibe.Domain.SupportRequests;
using Vibe.EF.Entities.SupportEntities;

namespace Vibe.Services.SupportRequests.Converters
{
    public static class SupportRequestConverter
    {
        public static SupportRequest ToDomain(this SupportRequestEntity entity)
        {
            return new SupportRequest(entity.Id, entity.Title, entity.Description, entity.ClientId, entity.EmployeeId, entity.OpenedAt,
                entity.LastEmployeeAnswerAt, entity.LastClientAnswerAt, entity.IsClosed);
        }

        public static SupportRequest[] ToDomain(this IEnumerable<SupportRequestEntity> entities)
        {
            return entities.Select(e => ToDomain(e)).ToArray();
        }
    }
}
