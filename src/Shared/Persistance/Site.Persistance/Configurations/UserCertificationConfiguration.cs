using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class UserCertificationConfiguration : IEntityTypeConfiguration<UserCertification>
  {
    public void Configure(EntityTypeBuilder<UserCertification> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.User).WithMany(x => x.UserCertifications).HasForeignKey(x => x.UserId);
      builder.HasOne(x => x.Certification).WithMany(x => x.UserCertifications).HasForeignKey(x => x.CertificationId);
    }
  }
}
