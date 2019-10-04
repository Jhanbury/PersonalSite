using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasOne<User>()
            //    .WithMany(x => x.Jobs)
            //    .HasForeignKey(x => x.UserId)
            //    .HasConstraintName("FK_Job_User_UserId")
            //    .OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne<Company>()
            //    .WithMany(x => x.Jobs)
            //    .HasForeignKey(x => x.CompanyId)
            //    .HasConstraintName("FK_Job_Company_CompanyId")
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}