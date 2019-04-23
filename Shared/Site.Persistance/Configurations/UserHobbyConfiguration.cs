using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class UserHobbyConfiguration : IEntityTypeConfiguration<UserHobby>
    {
        public void Configure(EntityTypeBuilder<UserHobby> builder)
        {
            builder.HasKey(x => new {x.HobbyId, x.UserId});
            builder.HasOne(x => x.Hobby).WithMany(x => x.UserHobbies);
            builder.HasOne(x => x.User).WithMany(x => x.UserHobbies);
        }
    }
}