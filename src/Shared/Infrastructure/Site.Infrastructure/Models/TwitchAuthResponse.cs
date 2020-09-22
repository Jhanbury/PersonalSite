using Newtonsoft.Json;

namespace Site.Infrastructure.Models
{
    public class TwitchAuthResponse
    {
        [JsonProperty("access_token")] public string Access_Token { get; set; }
        [JsonProperty("expires_in")] public int Expires_In { get; set; }
        [JsonProperty("token_type")] public string Token_Type { get; set; }
        
}
}