﻿using Vibe.Domain.Users;
using Vibe.Tools.Result;

namespace Vibe.Services.Users.Interface
{
    public interface IUserService
    {
        Result<Guid> SaveUserByClient(Guid clientId);
        User? GetUser(Guid userId);
        User? GetUserByRefreshToken(String refreshToken);
        User? GetUserByClientId(Guid clientId);
        Result UpdateUser(User user);
    }
}
