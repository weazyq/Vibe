using Microsoft.EntityFrameworkCore;
using Vibe.EF.Entities;

namespace Vibe.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Scooter> Scooters { get; set; }
        public DbSet<Workzone> Workzones { get; set; }
    }
}