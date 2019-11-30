using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
  public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
  {
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.Accreditor)
        .WithMany(x => x.Certifications)
        .HasForeignKey(x => x.AccreditorId);
    }
  }
}
