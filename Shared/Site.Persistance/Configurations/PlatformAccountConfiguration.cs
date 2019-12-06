using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
    public class PlatformAccountConfiguration : IEntityTypeConfiguration<PlatformAccount>
    {
        public void Configure(EntityTypeBuilder<PlatformAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.PlatformAccounts);
        }
    }
}
