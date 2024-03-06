using Vibe.Domain.Users;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Users.Converters;
using Vibe.Tools.Result;

namespace Vibe.EF
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User? GetUser(Guid userId)
        {
            UserEntity? userEntity = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (userEntity == null) return null;

            return userEntity.ToDomain();
        }

        public User? GetUserByRefreshToken(String refreshToken)
        {
            UserEntity? userEntity = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if(userEntity == null) return null;

            return userEntity.ToDomain();
        }

        public Result<Guid> SaveUserByClient(Guid clientId)
        {
            try
            {
                UserEntity user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    ClientId = clientId,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Add(user);
                _context.SaveChanges();
                return Result<Guid>.Success(user.Id);
            }
            catch (Exception e)
            {
                return Result.Fail("К сожалению не удалось создать твой аккаунт. Разработчики скоро всё починят.");
            }
        }

        public Result UpdateUser(User user)
        {
            UserEntity? userEntity = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userEntity is null) return Result.Fail("Пользователь не существует");

            try
            {
                userEntity.UpdateFromUser(user);
                _context.Users.Update(userEntity);
                _context.SaveChanges();
                return Result.Success;
            }
            catch (Exception e)
            {
                return Result.Fail("Возникли ошибки при обновлении пользователя");
            }
        }
    }
}
