using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Site.Application.Infrastructure.Models.Twitch;
using Site.Application.Interfaces;
using Site.Application.Interfaces.Messaging;
using Site.Domain.Entities;
using Site.Infrastructure.Messages;


namespace Site.Infrastructure.MessageHandlers
{
  public class TwitchWebhookSubscriptionHandler : IMessageHandler<IMessage>
  {
    private readonly ITwitchService _twitchService;
    private readonly IRepository<PlatformAccount, int> _repository;
    private readonly IConfiguration _configuration;

    public TwitchWebhookSubscriptionHandler(ITwitchService twitchService, IConfiguration config, IRepository<PlatformAccount, int> repository)
    {
      _twitchService = twitchService;
      _repository = repository;
      _configuration = config;
    }

    public async Task ProcessAsync(IMessage message)
    {
      if (message is TwitchWebhookSubscriptionMessage twitchWebhookSubscriptionMessage)
      {
        var twitchAccounts =
          await _repository.GetIncluding(
            x => x.UserId.Equals(twitchWebhookSubscriptionMessage.UserId) && x.Platform == Site.Domain.Enums.Platform.Twitch);
        foreach (var twitchAccount in twitchAccounts)
        {
          var parameter = new TwitchSubscriptionData()
          {
            UserId = twitchAccount.UserId,
            TwitchAccountId = twitchAccount.PlatformId
          };
          await _twitchService.SubscribeToTwitchStreamWebHook(parameter);
        }
        
      }
    }
  }
}
