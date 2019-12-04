using System;

namespace Site.Domain.Entities
{
  public class UserDegree
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DegreeId { get; set; }
    public int GradeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public User User { get; set; }
    public Grade Grade { get; set; }
    public Degree Degree { get; set; }
    
  }
}
