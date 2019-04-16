using Microsoft.EntityFrameworkCore;
using Site.Application.Entities;

namespace Site.Persistance
{
    public class SiteDbContext : DbContext
    {
        public SiteDbContext(DbContextOptions<SiteDbContext> options)
            : base(options)
        {
        }

        public DbSet<GithubRepo> GithubRepos { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<PersonalProject> PersonalProjects { get; set; }
        public DbSet<PersonalProjectTechnology> PersonalProjectTechnologies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<SocialMediaAccount> SocialMediaAccounts { get; set; }
        public DbSet<User> Users { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteDbContext).Assembly);
        }
    }
}