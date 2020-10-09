using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
  public class BlogPostTagsConfiguration : IEntityTypeConfiguration<BlogPostTag>
  {
    public void Configure(EntityTypeBuilder<BlogPostTag> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.UserBlogPost)
        .WithMany(x => x.BlogPostTags)
        .HasForeignKey(x => x.UserBlogPostId);
      builder.Property(x => x.Tag)
        .IsRequired();
    }
  }
}
