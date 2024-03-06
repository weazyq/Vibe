using Vibe.Domain.Scooter;
using Vibe.EF.Entities;

namespace Vibe.Services.Scooters.Converters
{
    public static class ScootersConverter
    {
        public static Scooter ToDomain(this ScooterEntity scooterEntity)
        {
            return new Scooter(scooterEntity.Id, scooterEntity.SerialNumber, scooterEntity.Latitude, scooterEntity.Longitude, scooterEntity.Charge, scooterEntity.State);
        }

        public static Scooter[] ToDomain(this IEnumerable<ScooterEntity> scooterEntities)
        {
            return scooterEntities.Select(s => s.ToDomain()).ToArray();
        }
    }
}
