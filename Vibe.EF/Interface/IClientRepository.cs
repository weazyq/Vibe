﻿using Vibe.Domain.Clients;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IClientRepository
    {
        Result<Guid> SaveClient(ClientBlank blank);
        Client? GetClient(Guid clientId);
        Client? GetClientByPhoneNumber(String phoneNumber);
        Client? GetClientByRefreshToken(String refreshToken);
        Result UpdateClient(Client client);
    }
}
