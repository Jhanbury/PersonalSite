using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class DegreeConfiguration : IEntityTypeConfiguration<Degree>
    {
        public void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasOne<User>()
            //    .WithMany(x => x.Degrees)
            //    .HasForeignKey(x => x.UserId)
            //    .HasConstraintName("FK_Degree_User_UserId")
            //    .OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne<University>()
            //    .WithMany(x => x.Degrees)
            //    .HasForeignKey(x => x.UniversityId)
            //    .HasConstraintName("FK_Degree_University_UniversityId")
            //    .OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne<Grade>()
            //    .WithMany(x => x.Degrees)
            //    .HasForeignKey(x => x.GradeId)
            //    .HasConstraintName("FK_Degree_Grade_GradeId")
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}