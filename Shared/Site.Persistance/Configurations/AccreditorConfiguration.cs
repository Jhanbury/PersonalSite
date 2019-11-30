using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

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
