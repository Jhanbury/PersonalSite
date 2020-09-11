using System.Threading;
using System.Threading.Tasks;
using Site.Application.Interfaces;
using Site.Application.Interfaces.Messaging;
using Site.Infrastructure.Messages;

namespace Site.Infrastructure.MessageHandlers
{
  public class VideoPlatformMessageHandler : IMessageHandler<IMessage>
  {
    private readonly ITwitchService _twitchService;
    private readonly IYouTubeService _youTubeService;

    public VideoPlatformMessageHandler(ITwitchService twitchService, IYouTubeService youTubeService)
    {
      _twitchService = twitchService;
      _youTubeService = youTubeService;
    }

    public async Task ProcessAsync(IMessage message)
    {
      if (message is VideoPlatformsMessage videoMessage)
      {
        await _twitchService.UpdateTwitchAccounts(videoMessage.UserID);
        await _twitchService.UpdateTwitchVideos(videoMessage.UserID);
        await _youTubeService.UpdateYouTubeAccounts(videoMessage.UserID);
        await _youTubeService.UpdateYouTubeVideos(videoMessage.UserID);
      }
    }
  }
}
