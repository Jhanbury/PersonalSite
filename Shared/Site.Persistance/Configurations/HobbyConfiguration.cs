using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class HobbyConfiguration : IEntityTypeConfiguration<Hobby>
    {
        public void Configure(EntityTypeBuilder<Hobby> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.HobbyType).WithMany(x => x.Hobbies);
        }
    }
}