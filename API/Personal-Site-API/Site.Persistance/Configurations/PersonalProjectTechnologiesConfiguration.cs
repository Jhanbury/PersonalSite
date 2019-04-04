using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class PersonalProjectTechnologiesConfiguration : IEntityTypeConfiguration<PersonalProjectTechnology>
    {
        public void Configure(EntityTypeBuilder<PersonalProjectTechnology> builder)
        {
            builder.HasKey(x => new {x.PersonalProjectId, x.TechnologyId});

            builder.HasOne(x => x.Technology)
                .WithMany(x => x.PersonalProjects)
                .HasForeignKey(x => x.TechnologyId)
                .IsRequired();

            builder.HasOne(x => x.PersonalProject)
                .WithMany(x => x.Technologies)
                .HasForeignKey(x => x.PersonalProjectId)
                .IsRequired();
        }
    }
}