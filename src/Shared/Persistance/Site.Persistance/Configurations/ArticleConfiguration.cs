using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class ArticleConfiguration : IEntityTypeConfiguration<Article>
  {
    public void Configure(EntityTypeBuilder<Article> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.User)
        .WithMany(x => x.Articles)
        .HasForeignKey(x => x.UserId);
    }
  }
}
