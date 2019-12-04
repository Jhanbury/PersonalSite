using System.Collections.Generic;
using Site.Domain.Enums;

namespace Site.Domain.Entities
{
    public class PlatformAccount
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string IconUrl { get; set; }
        public Platform Platform { get; set; }
        public string PlatformId { get; set; }
        public bool IsLive { get; set; }
        public int Followers { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Video> Videos { get; set; }
    }
}
