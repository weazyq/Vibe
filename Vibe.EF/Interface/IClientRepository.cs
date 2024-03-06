using Vibe.Domain.Clients;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IClientRepository
    {
        Result<Guid> SaveClient(ClientBlank blank);
        Client? GetClient(Guid clientId);
        Client GetClientByUser(Guid userId);
    }
}
