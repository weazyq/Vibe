using Vibe.Domain.Clients;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IClientRepository
    {
        DataResult<Guid> SaveClient(ClientBlank blank);
    }
}
