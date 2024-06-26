﻿using Vibe.Domain.Clients;
using Vibe.EF.Entities;

namespace Vibe.Services.Clients.Converters
{
    public static class ClientConverter
    {
        public static Client ToDomain(this ClientEntity entity)
        {
            return new Client(entity.Id, entity.Name, entity.Phone, entity.RefreshToken, entity.TokenCreated, entity.TokenExpires, entity.CreatedAt, 
                entity.ModifiedAt, entity.IsRemoved);
        }
    }
}
