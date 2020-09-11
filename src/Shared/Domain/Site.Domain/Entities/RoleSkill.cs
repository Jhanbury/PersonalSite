namespace Site.Domain.Entities
{
  public class RoleSkill
  {
    public int RoleId { get; set; }
    public int SkillId { get; set; }
    public virtual Skill Skill { get; set; }
    public virtual Role Role { get; set; }
  }
}
