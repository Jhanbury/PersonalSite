using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
    public class ProjectSkillsConfiguration : IEntityTypeConfiguration<ProjectSkill>
    {
        public void Configure(EntityTypeBuilder<ProjectSkill> builder)
        {
            builder.HasKey(x => new {x.ProjectId, x.SkillId});
            builder.HasOne<Project>()
                .WithMany(x => x.ProjectSkills)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Skill>()
                .WithMany(x => x.ProjectSkills)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
