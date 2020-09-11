using System.Collections.Generic;
using Newtonsoft.Json;

namespace Site.Application.Infrastructure.Models.Twitch
{
  public class TwitchStreamUpdateResponse
  {
    [JsonProperty("data")]
    public IEnumerable<TwitchStreamUpdateData> Data { get; set; }
  }
}
