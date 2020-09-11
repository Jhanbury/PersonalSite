using System.Collections.Generic;
using Newtonsoft.Json;

namespace Site.Application.Infrastructure.Models
{
    public class BlogApiResponse
    {
        [JsonProperty("posts")]
        public List<BlogPostResponse> Posts { get; set; }
    }
    public class BlogPostResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("feature_image")]
        public string ImageUrl { get; set; }
        [JsonProperty("custom_excerpt")]
        public string Teaser { get; set; }
    }
}