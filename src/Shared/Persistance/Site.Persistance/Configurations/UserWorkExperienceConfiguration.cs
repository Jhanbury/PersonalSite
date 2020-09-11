using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class UserWorkExperienceConfiguration : IEntityTypeConfiguration<UserWorkExperience>
  {
    public void Configure(EntityTypeBuilder<UserWorkExperience> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.User).WithMany(x => x.UserWorkExperiences).HasForeignKey(x => x.UserId);
      builder.HasOne(x => x.Company).WithMany(x => x.UserWorkExperiences).HasForeignKey(x => x.CompanyId);
      builder.HasOne(x => x.Role).WithMany(x => x.UserWorkExperiences).HasForeignKey(x => x.RoleId);
    }
  }
}
