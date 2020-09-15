using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class CompanyConfiguration : IEntityTypeConfiguration<Company>
  {
    public void Configure(EntityTypeBuilder<Company> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.Location).WithMany(x => x.Companies).HasForeignKey(x => x.LocationId);
    }
  }
}
