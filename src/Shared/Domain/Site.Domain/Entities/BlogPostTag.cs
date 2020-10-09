namespace Site.Domain.Entities
{
  public class BlogPostTag
  {
    public int Id { get; set; }
    public string Tag { get; set; }
    public int UserBlogPostId { get; set; }
    public UserBlogPost UserBlogPost { get; set; }
  }
}
