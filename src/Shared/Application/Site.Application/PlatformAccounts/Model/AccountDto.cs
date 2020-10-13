using System.Collections.Generic;
using Site.Application.Videos.Models;

namespace Site.Application.PlatformAccounts.Model
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string IconUrl { get; set; }
        public string Platform { get; set; }
        //public string PlatformId { get; set; }
        public bool IsLive { get; set; }
        public int Followers { get; set; }
        public List<VideoDto> Videos { get; set; }
    }
}
