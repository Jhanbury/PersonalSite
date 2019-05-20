using System.Collections;
using System.Collections.Generic;

namespace Site.Application.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProjectSkills> ProjectSkills { get; set; }
    }
}