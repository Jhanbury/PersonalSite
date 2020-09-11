using System;

namespace Site.Domain.Entities
{
  public class UserWorkExperience
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int RoleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentJob => !EndDate.HasValue;
    public User User { get; set; }
    public Company Company { get; set; }
    public Role Role { get; set; }


  }
}
