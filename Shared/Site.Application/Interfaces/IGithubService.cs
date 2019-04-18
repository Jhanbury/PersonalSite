using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;

namespace Site.Application.Interfaces
{
    public interface IGithubService
    {
        Task<List<GithubRepoApiResultDto>> GetAllGithubRepos(string username);
        Task UpdateGithubReposForUser(int userId, string username);
    }
}