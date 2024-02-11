using Microsoft.EntityFrameworkCore;
using Vibe.EF.Entities;

namespace Vibe.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<ScooterEntity> Scooters { get; set; }
        public DbSet<WorkzoneEntity> Workzones { get; set; }
        public DbSet<PhoneCodeEntity> PhoneCodes { get; set; }
    }
}