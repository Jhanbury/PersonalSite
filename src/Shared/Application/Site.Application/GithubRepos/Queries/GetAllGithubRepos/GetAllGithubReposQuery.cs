using System.Collections.Generic;
using MediatR;
using Site.Application.GithubRepos.Models;

namespace Site.Application.GithubRepos.Queries.GetAllGithubRepos
{
    public class GetAllGithubReposQuery : IRequest<List<GithubRepoDto>>
    {
    public int UserId { get; set; }

    public GetAllGithubReposQuery(int userId)
    {
      UserId = userId;
    }
  }
}
