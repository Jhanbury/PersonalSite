using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
  public class DegreeConfiguration : IEntityTypeConfiguration<Degree>
  {
    public void Configure(EntityTypeBuilder<Degree> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.University).WithMany(x => x.Degrees).HasForeignKey(x => x.UniversityId);
      builder.HasOne(x => x.DegreeType).WithMany(x => x.Degrees).HasForeignKey(x => x.DegreeTypeId);
    }
  }
}
