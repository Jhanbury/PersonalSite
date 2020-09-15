using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
    public class HobbyTypeConfiguration : IEntityTypeConfiguration<HobbyType>
    {
        public void Configure(EntityTypeBuilder<HobbyType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
