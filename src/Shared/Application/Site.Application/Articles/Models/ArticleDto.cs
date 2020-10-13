using System;

namespace Site.Application.Articles.Models
{
  public class ArticleDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string SiteName { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public int ReadLength { get; set; }
    public DateTime PublishDate { get; set; }
  }
}
