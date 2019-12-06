namespace Site.Domain.Entities
{
    public class SocialMediaAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AccountUrl { get; set; }
        public int SocialMediaPlatformId { get; set; }
        public User User { get; set; }
        public SocialMediaPlatform SocialMediaPlatform { get; set; }
    }
}