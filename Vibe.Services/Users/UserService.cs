using Vibe.Domain.Users;
using Vibe.EF.Interface;
using Vibe.Services.Users.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result<Guid> SaveUserByClient(Guid clientId)
        {
            return _userRepository.SaveUserByClient(clientId);
        }

        public User? GetUser(Guid userId)
        {
            return _userRepository.GetUser(userId);
        }

        public User? GetUserByRefreshToken(String refreshToken)
        {
            return _userRepository.GetUserByRefreshToken(refreshToken);
        }

        public Result UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }
    }
}
