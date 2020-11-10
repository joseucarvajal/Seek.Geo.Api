using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeekQ.Geo.Api.Models;

namespace SeekQ.Geo.Api.Data.EntityConfigurations
{
    public class StateConfiguration : IEntityTypeConfiguration<StateModel>
    {
        public void Configure(EntityTypeBuilder<StateModel> configuration)
        {
            configuration.HasKey(g => g.StateId);

            configuration.Property(g => g.StateId)
                .ValueGeneratedNever()
                .IsRequired();

            configuration.Property(g => g.StateName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
