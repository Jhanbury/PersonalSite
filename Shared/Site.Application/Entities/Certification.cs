using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Application.Entities
{
  public class Certification
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int AccreditorId { get; set; }
    public string Description { get; set; }
    public Accreditor Accreditor { get; set; }
    public ICollection<UserCertification> UserCertifications { get; set; }
  }
}
