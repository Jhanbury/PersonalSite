using System.Collections.Generic;

namespace Site.Domain.Entities
{
  public class Accreditor
  {
    public Accreditor()
    {
      Certifications = new List<Certification>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<Certification> Certifications { get; set; }
  }
}
