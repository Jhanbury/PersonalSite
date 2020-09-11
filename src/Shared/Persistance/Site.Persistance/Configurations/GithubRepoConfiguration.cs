using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Domain.Entities;

namespace Site.Persistance.Configurations
{
    public class GithubRepoConfiguration : IEntityTypeConfiguration<GithubRepo>
    {
        public void Configure(EntityTypeBuilder<GithubRepo> builder)
        {
            builder.HasKey(e => e.RepoId);
            builder.HasOne(x => x.Project)
                .WithOne(x => x.GithubRepo)
                .HasForeignKey<Project>(x => x.GithubRepoId);
        }
    }
}
