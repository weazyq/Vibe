using Microsoft.EntityFrameworkCore;

namespace Vibe.VirtualScooter.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<ScooterEntity> Scooters { get; set; }
        public DbSet<ScooterInfoEntity> ScooterInfos { get; set; }
    }
}