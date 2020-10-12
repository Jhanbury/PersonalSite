using System;
using System.Collections.Generic;
using Site.Application.Users.Models;

namespace Site.Application.BlogPosts.Models
{
  public class UserBlogPostDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public string Teaser { get; set; }
    public int Likes { get; set; }
    public int Comments { get; set; }
    public int Views { get; set; }
    public DateTime PublishDate { get; set; }
    public List<string> Tags { get; set; }
    public string Source { get; set; }
    //public int UserId { get; set; }
    public string AuthorName { get; set; }
    public string UserAvatar { get; set; }
    public int ReadLength { get; set; }

  }
}
