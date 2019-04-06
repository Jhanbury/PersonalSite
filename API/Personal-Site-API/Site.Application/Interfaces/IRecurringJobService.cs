using System.Threading.Tasks;

namespace Site.Application.Interfaces
{
    public interface IRecurringJobService
    {
        Task UpdateGithubRepos(int userId, string username);
    }
}