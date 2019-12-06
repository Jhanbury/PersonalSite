using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class DegreeTypeConfiguration : IEntityTypeConfiguration<DegreeType>
  {
    public void Configure(EntityTypeBuilder<DegreeType> builder)
    {
      builder.HasKey(x => x.Id);
    }
  }
}
