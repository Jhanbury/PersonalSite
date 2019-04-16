namespace Site.Application.Entities
{
    public class PersonalProjectTechnology
    {
        public int PersonalProjectId { get; set; }
        public int TechnologyId { get; set; }
        public Technology Technology { get; set; }
        public PersonalProject PersonalProject { get; set; }
    }
}