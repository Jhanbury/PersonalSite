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
        public DbSet<Address> Addresses { get; set; }
        public DbSet<SocialMediaAccount> SocialMediaAccounts { get; set; }
        public DbSet<UserBlogPost> UserBlogPosts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Hobby> Hobby { get; set; }
        public DbSet<UserHobby> UserHobby { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<ProjectTechnology> ProjectTechnologys { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Technology> Technologies { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteDbContext).Assembly);
        }
    }
}