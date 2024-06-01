using Microsoft.EntityFrameworkCore;
using Vibe.EF.Entities;
using Vibe.EF.Entities.SupportEntities;

namespace Vibe.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<PhoneCodeEntity> PhoneCodes { get; set; }
        public DbSet<RentEntity> Rents { get; set; }
        public DbSet<ScooterEntity> Scooters { get; set; }
        public DbSet<SupportRequestEntity> SupportRequests { get; set; }
        public DbSet<SupportMessageEntity> SupportMessages { get; set; }
    }
}