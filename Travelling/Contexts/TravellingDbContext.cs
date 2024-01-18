using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Travelling.Models;

namespace Travelling.Contexts
{
    public class TravellingDbContext : DbContext
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TravellingDbContext>(
                options => options.UseSqlServer("ConnectionStrings:MSSql"));
        }

        public TravellingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; }
    }
}
