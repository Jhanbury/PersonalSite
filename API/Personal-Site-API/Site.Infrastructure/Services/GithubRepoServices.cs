using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Application.GithubRepos.Models;
using Site.Application.Interfaces;

namespace Site.Infrastructure.Services
{
    public class GithubRepoServices : IGithubService
    {
        public Task<List<GithubRepoDto>> GetAllGithubRepos(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}