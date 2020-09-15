using System.Collections.Generic;

namespace Site.Domain.Entities
{
  public class University
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<Degree> Degrees { get; set; }
    
  }
}
