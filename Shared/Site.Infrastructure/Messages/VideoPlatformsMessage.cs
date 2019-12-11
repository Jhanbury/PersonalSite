using Site.Application.Interfaces.Messaging;
using TwitchLib.Api.V5.Models.Users;

namespace Site.Infrastructure.Messages
{
  public class VideoPlatformsMessage : Message
  {
    public int UserID { get; set; }
    public VideoPlatformsMessage(int userId) : base("VideoPlatformMessage", MessageType.VideoPlatformUpdate)
    {
      UserID = userId;
    }
  }
}
