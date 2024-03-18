
using DemoCRM.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DemoCRM.Model
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> User { get; set; }
        public DbSet<Menu> Menu { get; set; }
    }
}
