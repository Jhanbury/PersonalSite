﻿using System;
using System.Collections.Generic;

namespace Site.Domains.Entities
{
    public class GithubRepo
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public long StargazersCount { get; set; }

        public long WatchersCount { get; set; }

        public string Language { get; set; }

        public bool HasIssues { get; set; }

        public bool HasDownloads { get; set; }

        public bool HasWiki { get; set; }

        public long OpenIssuesCount { get; set; }

        public long Forks { get; set; }

        public long OpenIssues { get; set; }

        public long Watchers { get; set; }

        public User User { get; set; }
    }
}