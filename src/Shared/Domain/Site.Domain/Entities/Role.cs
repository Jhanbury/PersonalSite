using System.Collections.Generic;
using Site.Domain.Enums;

namespace Site.Domain.Entities
{
  public class Role
  {
    public Role()
    {
      UserWorkExperiences = new List<UserWorkExperience>();
      RoleTechnologies = new List<RoleTechnology>();
      RoleSkills = new List<RoleSkill>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public RoleLocationType RoleLocationType { get; set; }
    public ICollection<UserWorkExperience> UserWorkExperiences { get; set; }
    public ICollection<RoleTechnology> RoleTechnologies { get; set; }
    public ICollection<RoleSkill> RoleSkills { get; set; }
  }
}
