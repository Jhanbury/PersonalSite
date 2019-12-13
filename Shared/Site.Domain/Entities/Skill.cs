using System.Collections.Generic;

namespace Site.Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProjectSkill> ProjectSkills { get; set; }
        public ICollection<RoleSkill> RoleSkills { get; set; }
    }
}
