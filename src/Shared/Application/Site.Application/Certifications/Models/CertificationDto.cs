using System;

namespace Site.Application.Certifications.Models
{
  public class CertificationDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Accreditor { get; set; }
    public DateTime DateObtained { get; set; }
    
  }
}
