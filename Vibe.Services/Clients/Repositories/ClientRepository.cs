using Vibe.Domain.Clients;
using Vibe.EF;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Clients.Converters;
using Vibe.Tools.Result;

namespace Vibe.Services.Clients.Repositories
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

        public Client? GetClientByPhoneNumber(String phoneNumber)
        {
            return _context.Clients.Where(c => !c.IsRemoved).FirstOrDefault(c => c.Phone == phoneNumber)?.ToDomain();
        }

        public Client? GetClientByRefreshToken(String refreshToken)
        {
            return _context.Clients.Where(c => !c.IsRemoved).FirstOrDefault(c => c.RefreshToken == refreshToken)?.ToDomain();
        }

        public Client? GetClient(Guid id)
        {
            ClientEntity? client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null) return null;

            return client.ToDomain();
        }

        public Result UpdateClient(Client client)
        {
            ClientEntity? clientEntity = _context.Clients.FirstOrDefault(u => u.Id == client.Id);
            if (clientEntity is null) return Result.Fail("Клиент не существует");

            try
            {
                clientEntity.UpdateFromUser(client);
                _context.Clients.Update(clientEntity);
                _context.SaveChanges();
                return Result.Success;
            }
            catch (Exception e)
            {
                return Result.Fail("Возникли ошибки при обновлении клиента");
            }
        }
    }
}
