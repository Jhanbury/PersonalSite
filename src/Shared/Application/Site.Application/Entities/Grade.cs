using System.Collections.Generic;

namespace Site.Application.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UsGrade { get; set; }
        public ICollection<Degree> Degrees { get; set; }
        
    }
}