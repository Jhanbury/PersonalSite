using Autofac;
using Site.Application.Interfaces;
using Site.Application.Messaging;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Services;
using Site.Persistance.Repository;
using TwitchLib.Api;

namespace Site.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterType<GithubRepoServices>().As<IGithubService>();
            builder.RegisterType<BlogPostService>().As<IBlogPostService>();
            builder.RegisterType<YouTubeService>().As<IYouTubeService>();
            builder.RegisterType<TwitchService>().As<ITwitchService>();
            builder.RegisterType<RecurringJobService>().As<IRecurringJobService>();
            builder.RegisterType<GithubRepoServices>().As<IGithubService>();
            builder.RegisterGeneric(typeof(EFRepository<,>)).As(typeof(IRepository<,>));
            builder.RegisterType<GithubMessageHandler>().Keyed<IMessageHandler<IMessage>>(MessageType.GithubRepoUpdate);
            builder.RegisterType<BlogPostsMessageHandler>().Keyed<IMessageHandler<IMessage>>(MessageType.UserBlogPostsUpdate);
            builder.RegisterType<MessageHandlerFactory>().As<IMessageHandlerFactory>();
            
        }
    }
}