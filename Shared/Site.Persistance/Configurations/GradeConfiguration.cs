using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
  public class GradeConfiguration : IEntityTypeConfiguration<Grade>
  {
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
      builder.HasKey(x => x.Id);
      
    }
  }
}
