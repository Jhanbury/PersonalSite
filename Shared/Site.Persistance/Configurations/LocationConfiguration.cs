using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
  public class LocationConfiguration : IEntityTypeConfiguration<Location>
  {
    public void Configure(EntityTypeBuilder<Location> builder)
    {
      builder.HasKey(x => x.Id);
    }
  }
}
