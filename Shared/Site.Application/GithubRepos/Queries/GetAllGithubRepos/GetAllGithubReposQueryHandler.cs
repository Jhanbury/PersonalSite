using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.GithubRepos.Models;
using Site.Application.Interfaces;
using Site.Domain.Entities;

namespace Site.Application.GithubRepos.Queries.GetAllGithubRepos
{
    public class GetAllGithubReposQueryHandler : IRequestHandler<GetAllGithubReposQuery, List<GithubRepoDto>>
    {
        private readonly IRepository<GithubRepo, int> _repository;
        private readonly IMapper _mapper;

        public GetAllGithubReposQueryHandler(IRepository<GithubRepo,int> repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<List<GithubRepoDto>> Handle(GetAllGithubReposQuery request,
            CancellationToken cancellationToken)
        {
            var repos = await _repository.GetAll();
            return repos
                .Select(x => _mapper.Map<GithubRepoDto>(x))
                .ToList();
        }
    }
}
