using System.Collections.Generic;
using Site.Domain.Enums;

namespace Site.Domain.Entities
{
  public class Role
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public RoleLocationType RoleLocationType { get; set; }
    public ICollection<UserWorkExperience> UserWorkExperiences { get; set; }
  }
}
