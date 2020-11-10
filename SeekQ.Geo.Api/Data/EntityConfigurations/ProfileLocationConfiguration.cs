using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeekQ.Geo.Api.Models;

namespace SeekQ.Geo.Api.Data.EntityConfigurations
{
    public class ProfileLocationConfiguration : IEntityTypeConfiguration<ProfileLocationModel>
    {
        public void Configure(EntityTypeBuilder<ProfileLocationModel> configuration)
        {
            configuration.HasKey(g => g.Id);

            configuration.Property(g => g.Id)
                .ValueGeneratedNever()
                .IsRequired();

            configuration.Property<string>("CityId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CityId")
                .IsRequired(false);

            configuration.HasOne(c => c.City)
                .WithMany()
                .HasForeignKey("CityId");
        }
    }
}
