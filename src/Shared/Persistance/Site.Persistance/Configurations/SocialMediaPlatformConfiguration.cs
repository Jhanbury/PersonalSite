using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
    public class SocialMediaPlatformConfiguration : IEntityTypeConfiguration<SocialMediaPlatform>
    {
        public void Configure(EntityTypeBuilder<SocialMediaPlatform> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
