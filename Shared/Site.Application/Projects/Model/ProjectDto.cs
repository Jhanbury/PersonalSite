using System.Collections.Generic;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;
using Site.Application.Skills.Model;
using Site.Application.Technologies.Models;
using Site.Application.Users.Models;

namespace Site.Application.Projects.Model
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? GithubRepoId { get; set; }
        //public int ProjectTypeId { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectType { get; set; }
        public GithubRepoDto GithubRepo { get; set; }
        public UserDto User { get; set; }
        public IEnumerable<SkillDto> Skills { get; set; }
        public IEnumerable<TechnologyDto> Technologies { get; set; }
    }
}