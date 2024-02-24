using Vibe.Domain.Rents;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IRentRepository
    {
        public Guid Save(Guid clientId, Guid scooterId);
        public Rent? GetRent(Guid id);
        public Result UpdateRent(Rent rent);
    }
}
