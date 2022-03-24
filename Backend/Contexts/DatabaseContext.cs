using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Specialist> Specialists { get; set; } = null!;
        public DbSet<Device> Devices { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Maintenance> maintenances { get; set; } = null!;
        public DbSet<Models.Task> tasks { get; set; } = null!;
        public DbSet<Statechange> statechanges { get; set; } = null!;
    }
}
