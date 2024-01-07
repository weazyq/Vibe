namespace Vibe.Domain.Scooter
{
    public class ScooterView
    {
        public Guid Id { get; }
        public String Name { get; }
        public Double Latitude { get; }
        public Double Longitude { get; }
        public Double Charge { get; }
        public ScooterState State { get; }

        public ScooterView(Guid id, String name, Double latitude, Double longitude, Double charge, ScooterState state)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Charge = charge;
            State = state;
        }
    }
}
