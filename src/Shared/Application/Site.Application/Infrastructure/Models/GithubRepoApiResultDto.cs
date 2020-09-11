using System;
using Newtonsoft.Json;
using Site.Domain.Entities;

namespace Site.Application.Infrastructure.Models
{
    public class GithubRepoApiResultDto
    {
        [JsonProperty("id")]
        public long GithubId { get; set; }
        
        public string Name { get; set; }

        public string FullName { get; set; }
        [JsonProperty("fork")]
        public bool Fork { get; set; }

        public string Description { get; set; }
        [JsonProperty("html_url")]
        public string Url { get; set; }
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [JsonProperty("stargazers_count")]
        public long StargazersCount { get; set; }

        public long WatchersCount { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }

        public bool HasIssues { get; set; }

        public bool HasDownloads { get; set; }

        public bool HasWiki { get; set; }
        [JsonProperty("open_issues_count")]
        public long OpenIssuesCount { get; set; }
        [JsonProperty("forks")]
        public long Forks { get; set; }
        [JsonProperty("open_issues")]
        public long OpenIssues { get; set; }
        [JsonProperty("watchers")]
        public long Watchers { get; set; }

        public User User { get; set; }
    }
}
