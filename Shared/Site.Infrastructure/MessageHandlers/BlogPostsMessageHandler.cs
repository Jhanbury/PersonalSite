using System.Threading.Tasks;
using Site.Application.BlogPosts.Models;
using Site.Application.Interfaces;
using Site.Application.Messaging;
using Site.Infrastructure.Messages;

namespace Site.Infrastructure.MessageHandlers
{
    public class BlogPostsMessageHandler : IMessageHandler<IMessage>
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostsMessageHandler(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        public async Task ProcessAsync(IMessage message)
        {
            if (message is BlogPostMessage blogPostMessage)
            {
                await _blogPostService.UpdateBlogPostsForUser(blogPostMessage.UserId);
            }
            
        }
    }
}