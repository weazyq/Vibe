namespace Vibe.Domain.Scooter
{
    public class Scooter
    {
        public Guid Id { get; }
        public String SerialNumber { get; }
        public Double? Latitude { get; }
        public Double? Longitude { get; }
        public Double? Charge { get; }
        public ScooterState? State { get; }

        public Scooter(Guid id, String serialNumber, Double? latitude, Double? longitude, Double? charge, ScooterState? state)
        {
            Id = id;
            SerialNumber = serialNumber;
            Latitude = latitude;
            Longitude = longitude;
            Charge = charge;
            State = state;
        }
    }
}
