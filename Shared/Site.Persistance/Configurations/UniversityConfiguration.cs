using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class UniversityConfiguration : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}