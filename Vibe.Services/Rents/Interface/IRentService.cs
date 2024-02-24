using Vibe.Domain.Rents;
using Vibe.Tools.Result;

namespace Vibe.Services.Rents.Interface
{
    public interface IRentService
    {
        public Task<Result<Rent>> Initialize(Guid scooterId, Guid clientId);
        public Task<Result> EndRent(Guid rentId);
    }
}
