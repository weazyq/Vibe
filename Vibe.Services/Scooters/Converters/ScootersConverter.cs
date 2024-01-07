﻿using Vibe.Domain.Scooter;
using Vibe.EF.Entities;

namespace Vibe.Services.Scooters.Converters
{
    public static class ScootersConverter
    {
        public static Scooter ToDomain(this ScooterEntity scooterEntity)
        {
            return new Scooter(scooterEntity.Id, scooterEntity.Url, scooterEntity.SerialNumber);
        }

        public static Scooter[] ToDomain(this IEnumerable<ScooterEntity> scooterEntities)
        {
            return scooterEntities.Select(s => s.ToDomain()).ToArray();
        }
    }
}
