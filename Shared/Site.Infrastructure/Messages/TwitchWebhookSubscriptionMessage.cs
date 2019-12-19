using Site.Application.Interfaces;
using Site.Application.Interfaces.Messaging;
using Site.Domain.Entities;

namespace Site.Infrastructure.Messages
{
  public class TwitchWebhookSubscriptionMessage : Message
  {
    public int UserId { get; set; }
    public TwitchWebhookSubscriptionMessage() : base("TwitchWebHookJob", MessageType.TwitchWebhookSubscription)
    {
    }
  }
}
