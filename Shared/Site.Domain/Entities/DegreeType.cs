using System.Collections.Generic;

namespace Site.Domain.Entities
{
  public class DegreeType
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Degree> Degrees { get; set; }
  }
}
