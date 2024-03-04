using Microsoft.EntityFrameworkCore;
using Vibe.VirtualScooter.Data;
using Vibe.VirtualScooter.Modules;

namespace Vibe.VirtualScooter.Services
{
    public class ScooterScheduler : BackgroundService
    {
        private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(5));
        private readonly IServiceProvider _serviceProvider;

        public ScooterScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync() && !stoppingToken.IsCancellationRequested)
            {
                await TaskAsync();
            }
        }

        private async Task TaskAsync()
        {
            if(VirtualScooterData.Instance.ScooterId == null) return;

            ScooterInfoEntity scooterInfo = new()
            {
                ScooterId = (Guid)VirtualScooterData.Instance.ScooterId,
                Latitude = VirtualScooterData.Instance.Latitude,
                Longitude = VirtualScooterData.Instance.Longitude,
                Charge = VirtualScooterData.Instance.Battery.Charge,
                State = VirtualScooterData.Instance.State,
                CreatedAt = DateTime.UtcNow
            };

            using (var scope = _serviceProvider.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetService<DataContext>();
                if (dataContext is null) throw new NullReferenceException("Не удалось получить DataContext виртуальному самокату");

                ScooterInfoEntity? existScooterInfo = dataContext.ScooterInfos.FirstOrDefault(info => info.ScooterId == scooterInfo.ScooterId);
                
                if (existScooterInfo is null) await dataContext.ScooterInfos.AddAsync(scooterInfo);
                else {
                    existScooterInfo.Update(scooterInfo);
                    dataContext.ScooterInfos.Update(existScooterInfo);
                }

                await dataContext.SaveChangesAsync();
            }
        }
    }
}
