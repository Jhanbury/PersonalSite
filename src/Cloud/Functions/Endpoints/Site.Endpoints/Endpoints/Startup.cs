﻿using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Site.Application.Infrastructure.AutoMapper;
using Site.Application.Interfaces;
using Site.Application.Users.Queries;
using Site.Infrastructure;
using Site.Infrastructure.Services;
using Site.Persistance;
using Site.Persistance.Repository;

[assembly: FunctionsStartup(typeof(Endpoints.Startup))]
namespace Endpoints
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.Services.BuildServiceProvider().GetService<IOptions<ExecutionContextOptions>>().Value;
            var config = new ConfigurationBuilder()
                .SetBasePath(context.AppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets(typeof(Startup).Assembly)
                .Build();
            //var services = new ServiceCollection();
            builder.Services.AddHttpClient();
            //builder.useServiceProviderFactory
            var connectionString = config["DBConnectionString"];
            var blogAPIKey = config["DevtoAPIKey"];

            builder.Services.AddMediatR(typeof(GetUserInfoQuery).Assembly);


            builder.Services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));
            builder.Services.AddScoped<IRecurringJobService, RecurringJobService>();
            //services.AddHttpClient();
            //builder.Services.AddAutofac(autoFacBuilder =>
            //{
            //    autoFacBuilder.Populate(builder.Services);
            //    autoFacBuilder.RegisterModule<ApplicationModule>();
            //    //autoFacBuilder.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            //});
            //builder.Services.AddHttpClient("dev.to", client =>
            //{
            //    client.BaseAddress = new Uri("https://dev.to/");
            //    client.DefaultRequestHeaders.Add("api-key", blogAPIKey);

            //});
            //builder.Services.AddHttpClient("github", client =>
            //{
            //    client.BaseAddress = new Uri("https://api.github.com");
            //    client.DefaultRequestHeaders.Add("User-Agent", "Personal-Site");
            //});
            builder.Services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly, typeof(InfrastructureProfile).GetTypeInfo().Assembly });

            //builder.Services.AddScoped<GithubMessageHandler>();
            //builder.Services.AddScoped<BlogPostsMessageHandler>();
            //builder.Services.AddScoped<VideoPlatformMessageHandler>();
            //builder.Services.AddScoped<TwitchWebhookSubscriptionHandler>();
            //builder.Services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));

            //builder.Services.AddScoped<HandlerResolver>(sp => message =>
            //{
            //    switch (message)
            //    {
            //        case MessageType.GithubRepoUpdate:
            //            return sp.GetService<GithubMessageHandler>();
            //        case MessageType.UserBlogPostsUpdate:
            //            return sp.GetService<BlogPostsMessageHandler>();
            //        case MessageType.VideoPlatformUpdate:
            //            return sp.GetService<VideoPlatformMessageHandler>();
            //        case MessageType.TwitchWebhookSubscription:
            //            return sp.GetService<TwitchWebhookSubscriptionHandler>();
            //        default:
            //            return null;

            //    }
            //});
            //builder.servicesRegisterType<GithubRepoServices>().As<IGithubService>();
            //builder.RegisterType<BlogPostService>().As<IBlogPostService>();
            //builder.RegisterType<RecurringJobService>().As<IRecurringJobService>();
            //builder.RegisterType<GithubRepoServices>().As<IGithubService>();
            //builder.RegisterGeneric(typeof(EFRepository<,>)).As(typeof(IRepository<,>));
            //builder.RegisterType<GithubMessageHandler>().Keyed<IMessageHandler<IMessage>>(MessageType.GithubRepoUpdate);
            //builder.RegisterType<BlogPostsMessageHandler>().Keyed<IMessageHandler<IMessage>>(MessageType.UserBlogPostsUpdate);

            builder.Services.AddTransient<IGithubService, GithubRepoServices>();
            builder.Services.AddTransient<IBlogPostService, BlogPostService>();
            builder.Services.AddTransient<ITwitchService, TwitchService>();
            builder.Services.AddTransient<IYouTubeService, YouTubeService>();
            //builder.Services.AddTransient<GithubMessageHandler>();
            //builder.Services.AddTransient<GithubMessageHandler>();
            //builder.Services.AddTransient<IMessageHandlerFactory, MessageHandlerFactory>();
        }
    }
}