using Site.Domains.Enums;

namespace Site.Domains.Entities
{
    public class SocialMediaAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AccountUrl { get; set; }
        public AccountType Type { get; set; }
        public User User { get; set; }
    }
}