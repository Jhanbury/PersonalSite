namespace Site.Application.Entities
{
    public class UserBlogPost
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}