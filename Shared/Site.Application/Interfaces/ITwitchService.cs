using System.Threading.Tasks;
using Site.Application.Infrastructure.Models.Twitch;
using Site.Application.PlatformAccounts.Commands;

namespace Site.Application.Interfaces
{
    public interface ITwitchService
    {
        Task UpdateTwitchVideos(int userId);
        Task UpdateTwitchAccounts(int userId);
        UpdateAccountStreamStateCommand HandleTwitchStreamUpdateWebhook(TwitchStreamUpdateResponse response, int userId);
        Task<bool> SubscribeToTwitchStreamWebHook(TwitchSubscriptionData subscription);

    }
}
