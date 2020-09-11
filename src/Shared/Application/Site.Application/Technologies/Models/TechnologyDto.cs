namespace Site.Application.Technologies.Models
{
    public class TechnologyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string TechnologyType { get; set; }
        //public ICollection<PersonalProjectTechnology> PersonalProjects { get; set; }
    }
}