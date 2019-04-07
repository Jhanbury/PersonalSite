using System;
using Newtonsoft.Json;

namespace Site.Application.GithubRepos.Models
{
    public class GithubRepoDto
    {
        public int Id { get; set; }
        [JsonProperty("id")]
        public long GithubId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("html_url")]
        public Uri Url { get; set; }
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [JsonProperty("stargazers_count")]
        public long StargazersCount { get; set; }
        [JsonProperty("watchers_count")]
        public long WatchersCount { get; set; }
        [JsonProperty("language")]
        public object Language { get; set; }
        [JsonProperty("has_issues")]
        public bool HasIssues { get; set; }
        [JsonProperty("has_downloads")]
        public bool HasDownloads { get; set; }
        [JsonProperty("has_wiki")]
        public bool HasWiki { get; set; }
        [JsonProperty("open_issues_count")]
        public long OpenIssuesCount { get; set; }
        [JsonProperty("forks")]
        public long Forks { get; set; }
        [JsonProperty("open_issues")]
        public long OpenIssues { get; set; }
        [JsonProperty("watchers")]
        public long Watchers { get; set; }
    }
}