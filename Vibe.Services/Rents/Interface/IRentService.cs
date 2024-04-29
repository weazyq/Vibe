using Vibe.Domain.Rents;
using Vibe.Tools.Result;

namespace Vibe.Services.Rents.Interface
{
    public interface IRentService
    {
        Task<Result<Rent>> Initialize(Guid scooterId, Guid clientId);
        Task<Result> EndRent(Guid rentId);
        Rent? GetRentByClient(Guid clientId);
        Rent? GetActiveUserRent(Guid userId);
        Rent[] GetRentHistory(Guid userId);
    }
}
