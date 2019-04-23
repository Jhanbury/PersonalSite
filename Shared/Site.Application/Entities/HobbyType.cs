using System.Collections.Generic;

namespace Site.Application.Entities
{
    public class HobbyType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Hobby> Hobbies { get; set; }
    }
}