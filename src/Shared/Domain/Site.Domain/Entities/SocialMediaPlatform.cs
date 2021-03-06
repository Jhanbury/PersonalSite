using System.Collections.Generic;

namespace Site.Domain.Entities
{
    public class SocialMediaPlatform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SocialMediaAccount> SocialMediaAccounts { get; set; }
    }
}
