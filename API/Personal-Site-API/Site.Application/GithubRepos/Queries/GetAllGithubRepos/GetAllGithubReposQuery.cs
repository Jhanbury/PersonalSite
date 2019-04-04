using System.Collections.Generic;
using MediatR;
using Site.Application.GithubRepos.Models;

namespace Site.Application.GithubRepos.Queries.GetAllGithubRepos
{
    public class GetAllGithubReposQuery : IRequest<GithubRepoDto>, IRequest<List<GithubRepoDto>>
    {
        
    }
}