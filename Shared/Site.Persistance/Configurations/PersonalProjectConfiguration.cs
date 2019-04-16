using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class PersonalProjectConfiguration : IEntityTypeConfiguration<PersonalProject>
    {
        public void Configure(EntityTypeBuilder<PersonalProject> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasMany(x => x.Technologies).WithOne(x => x.PersonalProject).HasForeignKey(x => x.TechnologyId)
        }
    }
}