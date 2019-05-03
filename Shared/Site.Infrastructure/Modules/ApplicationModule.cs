using Autofac;
using Site.Application.Interfaces;
using Site.Application.Messaging;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Services;
using Site.Persistance.Repository;

namespace Site.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GithubRepoServices>().As<IGithubService>().InstancePerLifetimeScope();
            builder.RegisterType<BlogPostService>().As<IBlogPostService>().InstancePerLifetimeScope();
            builder.RegisterType<RecurringJobService>().As<IRecurringJobService>().InstancePerLifetimeScope();
            builder.RegisterType<GithubRepoServices>().As<IGithubService>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EFRepository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            builder.RegisterType<GithubMessageHandler>().Keyed<IMessageHandler<IMessage>>(MessageType.GithubRepoUpdate).InstancePerLifetimeScope();
            builder.RegisterType<BlogPostsMessageHandler>().Keyed<IMessageHandler<IMessage>>(MessageType.UserBlogPostsUpdate).InstancePerLifetimeScope();
            builder.RegisterType<MessageHandlerFactory>().As<IMessageHandlerFactory>().InstancePerLifetimeScope();
        }
    }
}