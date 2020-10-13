using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Articles.Models;
using Site.Application.Interfaces;
using Site.Domain.Entities;

namespace Site.Application.Articles.Querys.UserArticles
{
  public class UserArticlesQueryHandler : IRequestHandler<UserArticlesQuery, List<ArticleDto>>
  {
    private readonly IRepository<Article, int> _repository;
    private readonly IMapper _mapper;

    public UserArticlesQueryHandler(IRepository<Article, int> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<List<ArticleDto>> Handle(UserArticlesQuery request, CancellationToken cancellationToken)
    {
      var userArticles = await _repository.Get(x => x.UserId.Equals(request.UserId));
      return userArticles.Select(x => _mapper.Map<ArticleDto>(x)).ToList();
    }
  }
}
