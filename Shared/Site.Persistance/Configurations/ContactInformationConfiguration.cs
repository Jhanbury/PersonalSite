using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class ContactInformationConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithOne(x => x.ContactInformation)
                .HasForeignKey<User>(x => x.Id);

            builder.HasOne(x => x.Address)
                .WithMany(x => x.ContactInformations)
                .HasForeignKey(x => x.AddressId);
        }
    }
}