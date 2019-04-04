using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Application.GithubRepos.Models;

namespace Site.Application.Interfaces
{
    public interface IGithubService
    {
        Task<List<GithubRepoDto>> GetAllGithubRepos(string username);
    }
}