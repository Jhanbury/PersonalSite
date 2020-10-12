using System;
using System.Collections.Generic;

namespace Site.Domain.Entities
{
    public class UserBlogPost
    {
        public int BlogId { get; set; }
        public string SourceId { get; set; }
        public BlogSite Source { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string UserAvatar { get; set; }
        public string Teaser { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Views { get; set; }
        public DateTime PublishDate { get; set; }
        public int MinutesToRead { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<BlogPostTag> BlogPostTags { get; set; }
    }

    public enum BlogSite
    {
      DevTo
    }
}
