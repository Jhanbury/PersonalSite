using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class ProjectTechnologiesConfiguration : IEntityTypeConfiguration<ProjectTechnology>
    {
        public void Configure(EntityTypeBuilder<ProjectTechnology> builder)
        {
            builder.HasKey(x => new { x.ProjectId, x.TechnologyId });
            builder.HasOne<Project>()
                .WithMany(x => x.ProjectTechnologys)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Technology>()
                .WithMany(x => x.ProjectTechnologys)
                .HasForeignKey(x => x.TechnologyId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}