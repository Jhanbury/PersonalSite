using System.Collections.Generic;
using Site.Application.Enums;

namespace Site.Application.Entities
{
    public class PersonalProject
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ProjectUrl { get; set; }
        public ProjectType ProjectType { get; set; }
        public ICollection<PersonalProjectTechnology> Technologies { get;set; }
        public User User { get; set; }

    }
}