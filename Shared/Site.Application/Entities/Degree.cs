using System.Collections.Generic;

namespace Site.Application.Entities
{
  public class Degree
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UniversityId { get; set; }
    public University University { get; set; }
    public int DegreeTypeId { get; set; }
    public DegreeType DegreeType { get; set; }
    public ICollection<UserDegree> UserDegrees { get; set; }

  }
}
