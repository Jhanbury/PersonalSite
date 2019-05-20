using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class ProjectTechnologiesConfiguration : IEntityTypeConfiguration<ProjectTechnologies>
    {
        public void Configure(EntityTypeBuilder<ProjectTechnologies> builder)
        {
            builder.HasKey(x => new { x.ProjectId, x.TechnologyId });
            builder.HasOne<Project>()
                .WithMany(x => x.ProjectTechnologies)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Technology>()
                .WithMany(x => x.ProjectTechnologies)
                .HasForeignKey(x => x.TechnologyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}