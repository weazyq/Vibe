namespace Vibe.Domain.Scooter
{
    public class Scooter
    {
        public Guid Id { get; }
        public String Url { get; }
        public String SerialNumber { get; }

        public Scooter(Guid id, String url, string serialNumber)
        {
            Id = id;
            Url = url;
            SerialNumber = serialNumber;
        }
    }
}
