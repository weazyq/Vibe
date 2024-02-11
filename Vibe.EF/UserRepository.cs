using Vibe.EF.Entities;
using Vibe.EF.Interface;
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

        public Result SaveUserByClient(Guid clientId)
        {
            UserEntity user = new UserEntity
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Add(user);
            _context.SaveChanges();
            return Result.Success;
        }
    }
}
