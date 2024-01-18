using Cars_automobile.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cars_automobile.Contexts
{
    public class CarsAutomobileDbContext : IdentityDbContext
    {
        public CarsAutomobileDbContext(DbContextOptions<CarsAutomobileDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
