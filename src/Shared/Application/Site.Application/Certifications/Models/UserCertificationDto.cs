using System;

namespace Site.Application.Certifications.Models
{
  public class UserCertificationDto
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CertificationName { get; set; }
    public string CertificationDescription { get; set; }
    public string Accreditor { get; set; }
    public DateTime DateObtained { get; set; }
  }
}
