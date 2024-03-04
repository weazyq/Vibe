namespace Vibe.Domain.Scooter
{
    public class Scooter
    {
        public Guid Id { get; }
        public String SerialNumber { get; }

        public Scooter(Guid id, string serialNumber)
        {
            Id = id;
            SerialNumber = serialNumber;
        }
    }
}
