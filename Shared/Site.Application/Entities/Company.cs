using System.Collections.Generic;

namespace Site.Application.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}