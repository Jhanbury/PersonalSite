using System.Collections.Generic;
using Site.Domains.Enums;

namespace Site.Domains.Entities
{
    public class Technology
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public TechnologyType TechnologyType { get; set; }
        public ICollection<PersonalProjectTechnology> PersonalProjects { get; set; }
    }
}