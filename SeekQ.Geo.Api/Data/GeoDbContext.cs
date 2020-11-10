using Microsoft.EntityFrameworkCore;
using SeekQ.Geo.Api.Data.EntityConfigurations;
using SeekQ.Geo.Api.Models;

namespace SeekQ.Geo.Api.Data
{
    public class GeoDbContext : DbContext
    {

        public GeoDbContext(DbContextOptions<GeoDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProfileLocationModel> ProfileLocations { get; set; }
        public DbSet<StateModel> States { get; set; }
        public DbSet<CityModel> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ProfileLocationConfiguration());
            builder.ApplyConfiguration(new StateConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
        }
    }
}
