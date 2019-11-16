using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Videos.Models;

namespace Site.Persistance.Configurations
{
    public class VideosConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.PlatformAccount).WithMany(x => x.Videos);
        }
    }
}