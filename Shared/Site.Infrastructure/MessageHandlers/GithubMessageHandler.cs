using System.Threading.Tasks;
using Site.Application.Interfaces;
using Site.Application.Interfaces.Messaging;
using Site.Infrastructure.Messages;

namespace Site.Infrastructure.MessageHandlers
{
    public class GithubMessageHandler : IMessageHandler<IMessage>
    {
        private readonly IGithubService _githubService;

        public GithubMessageHandler(IGithubService githubService)
        {
            _githubService = githubService;
        }

        public async Task ProcessAsync(IMessage message)
        {
            if (message is GithubMessage githubMessage)
            {
                await _githubService.UpdateGithubReposForUser(githubMessage.UserId, githubMessage.UserName);
            }
        }
    }
}