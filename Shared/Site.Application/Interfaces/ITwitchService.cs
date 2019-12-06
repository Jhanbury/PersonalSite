using System.Threading.Tasks;

namespace Site.Application.Interfaces
{
    public interface ITwitchService
    {
        Task UpdateTwitchVideos(int userId);
        Task UpdateTwitchAccounts(int userId);
    }
}