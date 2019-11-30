namespace Site.Application.Entities
{
    public class UserBlogPost
    {
        public int BlogId { get; set; }
        public string SourceId { get; set; }
        public BlogSite Source { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Teaser { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public enum BlogSite
    {
      DevTo
    }
}
