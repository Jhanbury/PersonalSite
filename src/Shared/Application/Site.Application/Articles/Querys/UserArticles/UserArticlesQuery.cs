using System.Collections.Generic;
using MediatR;
using Site.Application.Articles.Models;

namespace Site.Application.Articles.Querys.UserArticles
{
  public class UserArticlesQuery : IRequest<List<ArticleDto>>
  {
    public UserArticlesQuery(int userId)
    {
      UserId = userId;
    }

    public int UserId { get; }
  }
}
