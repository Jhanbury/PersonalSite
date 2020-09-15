using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities.Audit;

namespace Site.Persistance.Configurations
{
  public class PerformanceLogConfiguration : IEntityTypeConfiguration<PerformanceLog>
  {
    public void Configure(EntityTypeBuilder<PerformanceLog> builder)
    {
      builder.HasKey(x => x.Id);
    }
  }
}
