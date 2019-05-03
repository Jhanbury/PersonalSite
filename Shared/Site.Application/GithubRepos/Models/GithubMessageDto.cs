using Site.Application.Messaging;

namespace Site.Application.GithubRepos.Models
{
    public class GithubMessage : IMessage
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MessageName => "GithubRepoJob";
    }
}