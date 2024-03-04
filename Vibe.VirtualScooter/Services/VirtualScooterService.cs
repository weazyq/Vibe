using System.Net;
using System.Web;
using System.Net.Sockets;
using Vibe.VirtualScooter.Data;
using Vibe.VirtualScooter.Modules;
using Microsoft.AspNetCore.Hosting.Server.Features;

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
            String? serialNumber = Environment.GetEnvironmentVariable("SCOOTER_SERIALNUMBER") ?? throw new ArgumentNullException();
            Double latitude = Double.Parse(Environment.GetEnvironmentVariable("SCOOTER_LATITUDE") ?? throw new ArgumentNullException());
            Double longitude = Double.Parse(Environment.GetEnvironmentVariable("SCOOTER_LONGITUDE") ?? throw new ArgumentNullException());

            VirtualScooterData.Instance.SetCoordinates(latitude, longitude);
            VirtualScooterData.Instance.SetSerialNumber(serialNumber);

            Console.WriteLine($"{nameof(serialNumber)}: {serialNumber}");
            Console.WriteLine($"{nameof(latitude)}: {latitude}");
            Console.WriteLine($"{nameof(longitude)}: {longitude}");

            ScooterEntity? entity = _dataContext.Scooters.Select(s => s)
                .Where(s => s.SerialNumber == serialNumber)
                .FirstOrDefault();

            if (entity == null)
            {
                entity = new()
                {
                    Id = Guid.NewGuid(),
                    SerialNumber = serialNumber,
                    CreatedAt = DateTime.UtcNow
                };

                _dataContext.Add(entity);
            }

            _dataContext.SaveChanges();

            VirtualScooterData.Instance.SetScooterId(entity!.Id);
        }
    }
}
