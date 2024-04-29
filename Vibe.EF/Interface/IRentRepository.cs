using Vibe.Domain.Rents;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IRentRepository
    {
        Guid Save(Guid clientId, Guid scooterId);
        Rent? GetRent(Guid id);
        Rent? GetRentByClient(Guid clientId);
        Rent? GetActiveRent(Guid clientId);
        Rent[] GetRentHistory(Guid clientId);
        Result UpdateRent(Rent rent);
    }
}
