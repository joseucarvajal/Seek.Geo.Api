using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeekQ.Geo.Api.Models;

namespace SeekQ.Geo.Api.Data.EntityConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<CityModel>
    {
        public void Configure(EntityTypeBuilder<CityModel> configuration)
        {
            configuration.HasKey(g => g.CityId);

            configuration.Property(g => g.CityId)
                .ValueGeneratedNever()
                .IsRequired();

            configuration.Property(g => g.CityName)
                .HasMaxLength(50)
                .IsRequired();

            configuration.Property<string>("StateId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StateId")
                .IsRequired(false);

            configuration.HasOne(c => c.State)
                .WithMany()
                .HasForeignKey("StateId");
        }
    }
}
