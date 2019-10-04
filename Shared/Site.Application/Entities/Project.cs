using System.Collections.Generic;

namespace Site.Application.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? GithubRepoId { get; set; }
        //public int ProjectTypeId { get; set; }
        public string ProjectUrl { get; set; }
        public Enums.ProjectType ProjectType { get; set; }
        public GithubRepo GithubRepo { get; set; }
        public User User { get; set; }
        public virtual ICollection<ProjectSkill> ProjectSkills { get; set; }
        public virtual ICollection<ProjectTechnology> ProjectTechnologies { get; set; }
    }
}