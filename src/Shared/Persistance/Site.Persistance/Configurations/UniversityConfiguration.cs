using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class UniversityConfiguration : IEntityTypeConfiguration<University>
  {
    public void Configure(EntityTypeBuilder<University> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.Location).WithMany(x => x.Universities).HasForeignKey(x => x.LocationId);
    }
  }
}
