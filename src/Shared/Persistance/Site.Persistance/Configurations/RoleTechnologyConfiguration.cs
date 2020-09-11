using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class RoleTechnologyConfiguration : IEntityTypeConfiguration<RoleTechnology>
  {
    public void Configure(EntityTypeBuilder<RoleTechnology> builder)
    {
      builder.HasKey(x => new {x.RoleId, x.TechnologyId});
      builder.HasOne(x => x.Technology).WithMany(x => x.RoleTechnologies).HasForeignKey(x => x.TechnologyId);
      builder.HasOne(x => x.Role).WithMany(x => x.RoleTechnologies).HasForeignKey(x => x.RoleId);
    }
  }
}
