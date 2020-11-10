using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SeekQ.Geo.Api.Data
{
    public class GeoDbContextFactory : IDesignTimeDbContextFactory<GeoDbContext>
    {
        public GeoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GeoDbContext>();
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=SeekQ.Geo;User Id=sa;Password=Password123",
                x => x.UseNetTopologySuite());

            return new GeoDbContext(optionsBuilder.Options);
        }
    }
}