using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Site.Application.Infrastructure.Models.Twitch
{
  public class TwitchStreamUpdateData
  {
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("user_id")]
    public string UserId { get; set; }

    [JsonProperty("user_name")]
    public string UserName { get; set; }

    [JsonProperty("game_id")]
    public string GameId { get; set; }

    [JsonProperty("community_ids")]
    public List<object> CommunityIds { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("viewer_count")]
    public long ViewerCount { get; set; }

    [JsonProperty("started_at")]
    public DateTime StartedAt { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("thumbnail_url")]
    public string ThumbnailUrl { get; set; }
  }
}
