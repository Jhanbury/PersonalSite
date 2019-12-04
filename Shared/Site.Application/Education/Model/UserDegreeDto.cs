using System;

namespace Site.Application.Education.Model
{
  public class UserDegreeDto
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    //public int DegreeId { get; set; }
    //public int GradeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    //public User User { get; set; }
    public string Grade { get; set; }
    public string Degree { get; set; }
    public string University { get; set; }
  }
}
