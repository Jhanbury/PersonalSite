using System.Threading.Tasks;

namespace Site.Application.Interfaces
{
    public interface IYouTubeService
    {
        Task UpdateYouTubeVideos(int userId);
        Task UpdateYouTubeAccounts(int userId);
    }
}