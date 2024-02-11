using Vibe.Domain.Clients;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
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

        public DataResult<Guid> SaveClient(ClientBlank blank)
        {
            ClientEntity client = new ClientEntity
            {
                Id = Guid.NewGuid(),
                Name = blank.Name,
                Phone = blank.Phone,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Clients.Add(client);
            _context.SaveChanges();
            return DataResult<Guid>.Success(client.Id);
        }
    }
}
