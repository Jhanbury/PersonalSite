using AutoMapper;
using Site.Domain.Entities;
using Site.Infrastructure.Models.Blogs;

namespace Site.Infrastructure.ValueResolvers
{
  public class ReadTimeResolver : IValueResolver<DevtoBlogApiResponse, UserBlogPost, int>
  {
    public int Resolve(DevtoBlogApiResponse source, UserBlogPost destination, int destMember, ResolutionContext context)
    {
      var content = source.Body;
      var wordCount = GetWordCount(content);
      return wordCount / 200;
    }

    private int GetWordCount(string body)
    {
      char[] delimiters = new char[] { ' ', '\r', '\n' };
      return body.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries).Length;
    }
  }
}
