using Vibe.Domain.Scooter;

namespace Vibe.Services.Scooters.Interface
{
    public interface IScootersRepository
    {
        Scooter GetScooter(Guid id);
        ScooterView GetScooterView(Guid id);
    }
}
