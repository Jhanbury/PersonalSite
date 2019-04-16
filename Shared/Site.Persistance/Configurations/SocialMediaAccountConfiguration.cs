using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class SocialMediaAccountConfiguration : IEntityTypeConfiguration<SocialMediaAccount>
    {
        public void Configure(EntityTypeBuilder<SocialMediaAccount> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}