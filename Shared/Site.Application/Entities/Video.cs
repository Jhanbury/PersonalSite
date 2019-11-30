using System;
using Site.Application.PlatformAccounts.Model;

namespace Site.Application.Videos.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Url { get; set; }
        public int ViewCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int PlatformAccountId { get; set; }
        public string SourceId { get; set; }
        public string VideoDuration { get; set; }
        public PlatformAccount PlatformAccount { get; set; }
    }   
}