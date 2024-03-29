﻿using Vibe.Domain.Clients;
using Vibe.Tools.Result;

namespace Vibe.Services.Clients.Interface
{
    public interface IClientService
    {
        Result<Guid> SaveClient(ClientBlank clientBlank);
        Client? GetClient(Guid clientId);
        Client GetClientByUser(Guid userId);
        Result SendSms(String phoneNumber);
        Result CheckSms(ClientBlank blank, String code);
    }
}
