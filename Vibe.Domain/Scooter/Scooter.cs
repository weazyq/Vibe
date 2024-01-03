namespace Vibe.Domain.Scooter
{
    public class Scooter
    {
        public Guid Id { get; }
        public String Name { get; }
        public String Ip { get; }
        public String Port { get; }

        public Scooter(Guid id, String name, String ip, String port)
        {
            Id = id;
            Name = name;
            Ip = ip;
            Port = port;
        }
    }
}
