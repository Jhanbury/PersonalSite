using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class AccreditorConfiguration : IEntityTypeConfiguration<Accreditor>
  {
    public void Configure(EntityTypeBuilder<Accreditor> builder)
    {
      builder.HasKey(x => x.Id);
    }
  }
}
