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
        public DbSet<Maintenance> Maintenances { get; set; } = null!;
        public DbSet<Models.Task> Tasks { get; set; } = null!;
        public DbSet<Statechange> Statechanges { get; set; } = null!;
        public DbSet<Speciality> Specialities { get; set; } = null!;
    }
}
