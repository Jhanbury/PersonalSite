using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Application.Entities;

namespace Site.Persistance.Configurations
{
    public class GithubRepoConfiguration : IEntityTypeConfiguration<GithubRepo>
    {
        public void Configure(EntityTypeBuilder<GithubRepo> builder)
        {
            builder.HasKey(e => e.Id);

           
        }
    }
}