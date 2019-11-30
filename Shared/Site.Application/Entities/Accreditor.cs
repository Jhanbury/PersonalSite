using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Application.Entities
{
  public class Accreditor
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<Certification> Certifications { get; set; }
  }
}
