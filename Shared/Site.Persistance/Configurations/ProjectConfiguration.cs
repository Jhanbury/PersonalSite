using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne<User>()
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_Project_User_UserId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
