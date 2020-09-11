using System.Collections.Generic;

namespace Site.Domain.Entities
{
  public class Company
  {
    public Company()
    {
      UserWorkExperiences = new List<UserWorkExperience>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<UserWorkExperience> UserWorkExperiences { get; set; }


  }
}
