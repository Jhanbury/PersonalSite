using Microsoft.EntityFrameworkCore;
using Site.Domain.Entities;
using Site.Domain.Entities.Audit;

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
        public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<PlatformAccount> PlatformAccounts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<DegreeType> DegreeTypes { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<UserDegree> UserDegrees { get; set; }
        public DbSet<RoleTechnology> RoleTechnologies { get; set; }
        public DbSet<RoleSkill> RoleSkills { get; set; }
        public DbSet<UserWorkExperience> UserWorkExperiences { get; set; }
        public DbSet<PerformanceLog> PerformanceLogs { get; set; }



        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiteDbContext).Assembly);
        }
    }
}
