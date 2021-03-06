using System;

namespace Site.Domain.Entities
{
  public class Article
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string SiteName { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public int ReadLength { get; set; }
    public DateTime PublishDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

  }
}
