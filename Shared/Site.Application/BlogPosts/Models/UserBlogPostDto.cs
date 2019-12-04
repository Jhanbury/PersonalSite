using Site.Application.Users.Models;

namespace Site.Application.BlogPosts.Models
{
    public class UserBlogPostDto
    {
        public int Id { get; set; }
        public string SourceId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Teaser { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
