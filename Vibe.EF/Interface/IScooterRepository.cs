using Vibe.Domain.Scooter;

namespace Vibe.EF.Interface
{
    public interface IScooterRepository
    {
        public Scooter? GetScooter(Guid id);
        public Scooter[] GetScooters();
    }
}
