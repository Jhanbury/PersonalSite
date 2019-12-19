using System.Threading.Tasks;

namespace Site.Application.Interfaces
{
    public interface IRecurringJobService
    {
        Task UpdateGithubRepos(int userId, string username);
        Task UpdateUserBlogs(int userId);
        Task UpdateVideoPlatforms(int userId);
        Task SubscribeToTwitchWebhooks(int userId);
    }
}
