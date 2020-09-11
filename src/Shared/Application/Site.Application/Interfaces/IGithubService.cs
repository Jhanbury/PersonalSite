using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Application.Infrastructure.Models;
using Site.Domain.Entities;

namespace Site.Application.Interfaces
{
    public interface IGithubService
    {
        Task<List<GithubRepoApiResultDto>> GetAllGithubRepos(string username);
        Task UpdateGithubReposForUser(int userId, string username);
        void AddNewRepos(IEnumerable<GithubRepoApiResultDto> newRepos, int userId);
        Task RemoveDeletedRepos(IEnumerable<long> itemsToRemove);
        Task UpdateExisting(IEnumerable<GithubRepoApiResultDto> itemsToCheckForUpdates);

        IEnumerable<long> CalculateItemsToRemove(IEnumerable<GithubRepoApiResultDto> apiResults, IEnumerable<GithubRepo> dbRecords);
        IEnumerable<GithubRepoApiResultDto> CalculateItemsToAdd(IEnumerable<GithubRepoApiResultDto> apiResults, IEnumerable<GithubRepo> dbRecords);
        IEnumerable<GithubRepoApiResultDto> CalculateItemsToUpdate(IEnumerable<GithubRepoApiResultDto> apiResults, IEnumerable<GithubRepo> dbRecords);
    }
}
