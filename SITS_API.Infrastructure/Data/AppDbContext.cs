using Microsoft.EntityFrameworkCore;
using SITS_API.Domain.Entities;

namespace SITS_API.Infrastructure.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
    }
}
