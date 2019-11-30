using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
  public class UserDegreeConfiguration : IEntityTypeConfiguration<UserDegree>
  {
    public void Configure(EntityTypeBuilder<UserDegree> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.Degree).WithMany(x => x.UserDegrees).HasForeignKey(x => x.DegreeId);
      builder.HasOne(x => x.Grade).WithMany(x => x.UserDegrees).HasForeignKey(x => x.GradeId);
      builder.HasOne(x => x.User).WithMany(x => x.UserDegrees).HasForeignKey(x => x.UserId);
    }
  }
}
