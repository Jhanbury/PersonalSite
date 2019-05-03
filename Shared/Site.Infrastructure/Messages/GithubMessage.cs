using Site.Application.Messaging;

namespace Site.Infrastructure.Messages
{
    public class GithubMessage : Message
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public GithubMessage() : base("GithubReposJob", MessageType.GithubRepoUpdate)
        {
        }
    }
}