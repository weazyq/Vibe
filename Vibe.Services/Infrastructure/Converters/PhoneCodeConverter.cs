using Vibe.EF.Entities;

namespace Vibe.Domain.Infrastructure.Converters
{
    public static class PhoneCodeConverter
    {
        public static PhoneCode ToDomain(this PhoneCodeEntity entity)
        {
            return new PhoneCode(entity.Phone, entity.Code, entity.ValidityMinutes, entity.CreatedAt);
        }
    }
}
