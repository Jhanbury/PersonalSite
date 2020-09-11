using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasOne<ContactInformation>()
            //    .WithOne(x => x.User);

            builder.HasMany(x => x.GithubRepos)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.Address)
                .WithMany(x => x.Users)
                .IsRequired();
            

            builder.HasMany(x => x.SocialMediaAccounts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}
