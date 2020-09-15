using System;

namespace Site.Domain.Entities
{
  public class UserCertification
  {
    public int Id { get; set; }
    public int CertificationId { get; set; }
    public int UserId { get; set; }
    public DateTime DateObtained { get; set; }
    public Certification Certification { get; set; }
    public User User { get; set; }
  }
}
