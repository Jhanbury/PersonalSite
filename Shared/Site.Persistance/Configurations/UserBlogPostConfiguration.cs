using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class UserBlogPostConfiguration : IEntityTypeConfiguration<UserBlogPost>
    {
        public void Configure(EntityTypeBuilder<UserBlogPost> builder)
        {
            builder.ToTable("UserBlogPosts");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.UserBlogPosts);
        }
    }
}