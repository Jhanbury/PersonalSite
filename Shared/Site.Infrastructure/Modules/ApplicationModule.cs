
using Autofac;
using DryIoc;
using Site.Application.Infrastructure;
using Site.Application.Interfaces;
using Site.Application.Interfaces.Messaging;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Services;
using Site.Persistance.Repository;
using TwitchLib.Api;

namespace Site.Infrastructure.Modules
{
    public class ApplicationModule : IModule
    {
        public void Load(IRegistrator builder)
        {
          builder.Register<IGithubService, GithubRepoServices>();
          builder.Register<IBlogPostService, BlogPostService>();
          builder.Register<IYouTubeService, YouTubeService>();
          builder.Register<ITwitchService, TwitchService>();
          builder.Register<IRecurringJobService, RecurringJobService>();
          builder.Register<IGithubService, GithubRepoServices>();
          builder.Register<IMessageHandler<IMessage>, GithubMessageHandler>(serviceKey: MessageType.GithubRepoUpdate);
          builder.Register<IMessageHandler<IMessage>, BlogPostsMessageHandler>(serviceKey: MessageType.UserBlogPostsUpdate);
          builder.Register<IMessageHandlerFactory, MessageHandlerFactory>();
          builder.Register(typeof(IRepository<,>), typeof(EFRepository<,>));

    }
    }
}
