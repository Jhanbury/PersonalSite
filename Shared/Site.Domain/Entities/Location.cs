using System.Collections.Generic;

namespace Site.Domain.Entities
{
  public class Location
  {
    public int Id { get; set; }
    public string DisplayName { get; set; }
    public string CountryName { get; set; }
    public string CityName { get; set; }

    public ICollection<University> Universities { get; set; }
    public ICollection<Company> Companies { get; set; }
  }
}
