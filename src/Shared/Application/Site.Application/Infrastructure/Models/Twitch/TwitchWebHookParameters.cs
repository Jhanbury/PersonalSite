using Newtonsoft.Json;

namespace Site.Application.Infrastructure.Models.Twitch
{
  public class TwitchWebHookParameters
  {
    [JsonProperty("hub.callback")]
    public string Callback { get; set; }
    [JsonProperty("hub.mode")]
    public string Mode { get; set; }
    [JsonProperty("hub.topic")]
    public string Topic { get; set; }
    [JsonProperty("hub.lease_seconds")]
    public int Lease { get; set; }
  }
}
