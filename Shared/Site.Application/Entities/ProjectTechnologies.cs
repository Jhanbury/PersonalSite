namespace Site.Application.Entities
{
    public class ProjectTechnologies
    {
        public int ProjectId { get; set; }
        public int TechnologyId { get; set; }
        public Project Project { get; set; }
        public Technology Technology { get; set; }
    }
}