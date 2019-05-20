namespace Site.Application.Entities
{
    public class ProjectSkills
    {
        public int ProjectId { get; set; }
        public int SkillId { get; set; }
        public Project Project { get; set; }
        public Skill Skill { get; set; }
    }
}