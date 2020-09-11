using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class RoleSkillConfiguration : IEntityTypeConfiguration<RoleSkill>
  {
    public void Configure(EntityTypeBuilder<RoleSkill> builder)
    {
      builder.HasKey(x => new {x.SkillId, x.RoleId});
      builder.HasOne(x => x.Skill).WithMany(x => x.RoleSkills).HasForeignKey(x => x.SkillId);
      builder.HasOne(x => x.Role).WithMany(x => x.RoleSkills).HasForeignKey(x => x.RoleId);
    }
  }
}
