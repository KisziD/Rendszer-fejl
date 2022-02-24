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
    }
}
