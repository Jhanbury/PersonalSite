using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.BlogPosts.Models;
using Site.Application.Interfaces;
using Site.Domain.Entities;

namespace Site.Application.BlogPosts.Queries.GetUserBlogPosts
{
    public class GetUserBlogPostsQueryHandler : IRequestHandler<GetUserBlogPostsQuery,List<UserBlogPostDto>>
    {
        private readonly IRepository<UserBlogPost, string> _repository;
        private readonly IMapper _mapper;

        public GetUserBlogPostsQueryHandler(IRepository<UserBlogPost, string> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserBlogPostDto>> Handle(GetUserBlogPostsQuery request, CancellationToken cancellationToken)
        {
            var models = await _repository.GetIncluding(x => x.UserId.Equals(request.UserId), x => x.User);
            return models.Select(x => _mapper.Map<UserBlogPostDto>(x)).ToList();
        }
    }
}
