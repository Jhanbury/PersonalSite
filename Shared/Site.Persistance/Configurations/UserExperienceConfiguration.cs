using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
    public class UserExperienceConfiguration : IEntityTypeConfiguration<UserExperience>
    {
        public void Configure(EntityTypeBuilder<UserExperience> builder)
        {
            builder.HasKey(x => new {x.UserId, x.CompanyId, x.StartDate});
            builder.HasOne(x => x.User).WithMany(x => x.UserExperiences);
            builder.HasOne(x => x.Company).WithMany(x => x.UserExperiences);
        }
    }
}
