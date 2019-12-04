using System.Collections.Generic;

namespace Site.Domain.Entities
{
  public class Grade
  {
    public int Id { get; set; }
    public string DisplayName { get; set; }
    public string FinalGrade { get; set; }

    public ICollection<UserDegree> UserDegrees { get; set; }
  }
}
