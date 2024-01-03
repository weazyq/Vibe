namespace Vibe.Domain.Scooter
{
    public class ScooterView
    {
        public Guid Id { get; }
        public String Name { get; }
        public Double Longitude { get; }
        public Double Latitude { get; }
        public Double Charge { get; }
        public ScooterState State { get; }

        public ScooterView(Guid id, String name, Double longitude, Double latitude, Double charge, ScooterState state)
        {
            Id = id;
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
            Charge = charge;
            State = state;
        }
    }
}
