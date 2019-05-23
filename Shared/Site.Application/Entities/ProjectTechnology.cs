namespace Site.Application.Entities
{
    public class ProjectTechnology
    {
        public int ProjectId { get; set; }
        public int TechnologyId { get; set; }
        public virtual Project Project { get; set; }
        public virtual Technology Technology { get; set; }
    }
}