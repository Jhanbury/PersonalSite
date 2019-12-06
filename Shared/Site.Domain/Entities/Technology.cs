using System.Collections.Generic;
using Site.Domain.Enums;

namespace Site.Domain.Entities
{
    public class Technology
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public TechnologyType TechnologyType { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnologys { get; set; }
        
    }
}
