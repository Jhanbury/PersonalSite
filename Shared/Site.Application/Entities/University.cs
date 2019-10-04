using System.Collections.Generic;

namespace Site.Application.Entities
{
    public class University
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string  ShortName { get; set; }
        public string  City { get; set; }
        public string  Country { get; set; }
        public ICollection<Degree> Degrees { get; set; }
    }
}