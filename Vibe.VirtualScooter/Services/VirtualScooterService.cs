using System.Net;
using System.Net.Sockets;
using Vibe.Domain.Scooter;
using Vibe.VirtualScooter.Data;
using Vibe.VirtualScooter.Modules;

namespace Vibe.VirtualScooter.Services
{
    public class VirtualScooterService
    {
        private DataContext _dataContext { get; init; }

        public VirtualScooterService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Initialize()
        {
            String? currentUrl = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
            String? serialNumber = Environment.GetEnvironmentVariable("SCOOTER_SERIALNUMBER") ?? throw new ArgumentNullException();
            Double latitude = Double.Parse(Environment.GetEnvironmentVariable("SCOOTER_LATITUDE") ?? throw new ArgumentNullException());
            Double longitude = Double.Parse(Environment.GetEnvironmentVariable("SCOOTER_LONGITUDE") ?? throw new ArgumentNullException());

            VirtualScooterData.Instance.SetCoordinates(latitude, longitude);
            VirtualScooterData.Instance.SetSerialNumber(serialNumber);

            if (String.IsNullOrEmpty(currentUrl))
            {
                (String ip, String port) = GetLocalIPv4Address();
                currentUrl = $"https://{ip}:{port}";
            }

            ScooterEntity? entity = _dataContext.Scooters.Select(s => s)
                .Where(s => s.SerialNumber == serialNumber)
                .FirstOrDefault();

            if (entity == null)
            {
                entity = new()
                {
                    Id = Guid.NewGuid(),
                    Url = currentUrl,
                    SerialNumber = serialNumber,
                    CreatedAt = DateTime.UtcNow
                };

                _dataContext.Add(entity);
            }
            else if (entity != null && entity.Url != currentUrl)
            {
                entity.Url = currentUrl;
                entity.ModifiedAt = DateTime.UtcNow;
                _dataContext.Update(entity);
            }

            _dataContext.SaveChanges();

            VirtualScooterData.Instance.SetScooterId(entity!.Id);
        }

        public static (String, String) GetLocalIPv4Address(String? targetIp = null)
        {
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
            socket.Connect(targetIp ?? "8.8.8.8", 65530);
            IPEndPoint endPoint = (IPEndPoint)socket.LocalEndPoint;
            return (endPoint.Address.ToString(), endPoint.Port.ToString());
        }

        public string GetLocalIp()
        {
            IPAddress[] ipAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ip = ipAddresses.Select(ip => ip).Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).First();

            return ip.ToString();
        }

        public string GetLocalPort()
        {
            var listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();

            return port.ToString();
        }
    }
}
