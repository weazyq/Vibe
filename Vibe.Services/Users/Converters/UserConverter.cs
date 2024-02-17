using Vibe.Domain.Users;
using Vibe.EF.Entities;

namespace Vibe.Services.Users.Converters
{
    public static class UserConverter
    {
        public static User ToDomain(this UserEntity userEntity)
        {
            return new User(userEntity.Id, userEntity.EmployeeId, userEntity.ClientId, userEntity.RefreshToken, userEntity.TokenCreated, userEntity.TokenExpires);
        }
    }
}
