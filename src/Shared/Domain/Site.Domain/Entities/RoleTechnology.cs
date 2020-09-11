namespace Site.Domain.Entities
{
  public class RoleTechnology
  {
    public int RoleId { get; set; }
    public int TechnologyId { get; set; }
    public virtual Role Role { get; set; }
    public virtual Technology Technology { get; set; }
  }
}
