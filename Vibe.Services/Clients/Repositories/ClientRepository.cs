using Vibe.Domain.Clients;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Clients.Converters;
using Vibe.Tools.Result;

namespace Vibe.EF
{
    public class ClientRepository : IClientRepository
    {
        private DataContext _context;
        
        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public Result<Guid> SaveClient(ClientBlank blank)
        {
            try
            {
                ClientEntity client = new()
                {
                    Id = Guid.NewGuid(),
                    Name = blank.Name,
                    Phone = blank.Phone,
                    CreatedAt = DateTime.UtcNow,
                };

                _context.Clients.Add(client);
                _context.SaveChanges();
                return Result<Guid>.Success(client.Id);
            }
            catch (Exception e)
            {
                return Result.Fail("К сожалению не удалось создать твой аккаунт. Разработчики скоро всё починят.");
            }
        }

        public Client? GetClient(Guid id)
        {
            ClientEntity? client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null) return null;

            return client.ToDomain();
        }
    }
}
