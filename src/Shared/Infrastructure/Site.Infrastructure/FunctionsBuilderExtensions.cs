using System;
using System.Reflection;
using AutoMapper;
using Azure.Identity;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Site.Application.Infrastructure.AutoMapper;
using Site.Application.Interfaces;
using Site.Application.Interfaces.Messaging;
using Site.Application.Users.Queries.GetUserInfo;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Services;
using Site.Persistance;
using Site.Persistance.Repository;

namespace Site.Infrastructure
{
    public delegate IMessageHandler<IMessage> HandlerResolver(MessageType message);
    public static class FunctionsBuilderExtensions
    {

        public static IFunctionsHostBuilder ConfigureServices(this IFunctionsHostBuilder builder)
        {
            var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            //ef
            var connectionString = config["ConnectionString"];
            var twitchBaseAddress = config["Twitch:Endpoints:APIUrl"];
            var twitchAPIKey = config["Twitch:App:Id"];
            builder.Services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));
            //Http settings
            var blogAPIKey = config["DevtoAPIKey"];
            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient("dev.to", client =>
            {
                client.BaseAddress = new Uri("https://dev.to/");
                client.DefaultRequestHeaders.Add("api-key", blogAPIKey);

            });
            builder.Services.AddHttpClient("github", client =>
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.Add("User-Agent", "Personal-Site");
            });
            builder.Services.AddHttpClient("twitch", client =>
            {
                client.BaseAddress = new Uri(twitchBaseAddress);
                client.DefaultRequestHeaders.Add("client-id", twitchAPIKey);
            });
            //mediatr
            builder.Services.AddMediatR(typeof(GetUserInfoQuery).Assembly);
            //automapper
            builder.Services.AddAutoMapper(new[]
            {
                typeof(AutoMapperProfile).GetTypeInfo().Assembly,
                typeof(InfrastructureProfile).GetTypeInfo().Assembly
            });
            return builder;
        }

        public static IFunctionsHostBuilder ConfigureSerilog(this IFunctionsHostBuilder builder)
        {
            var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            //ef
            var connectionString = config["ConnectionString"];
            var logger = new LoggerConfiguration()
              .WriteTo.MSSqlServer(connectionString, new SinkOptions() { AutoCreateSqlTable = false, TableName = "Logs" })
              .CreateLogger();
            builder.Services.AddLogging(lb => lb.AddSerilog(logger));
            return builder;
        }


        public static IFunctionsHostBuilder ConfigureSchedulerServices(this IFunctionsHostBuilder builder)
        {
            var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = config["ConnectionString"];
            builder.Services.AddHttpClient();
            builder.Services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));
            builder.Services.AddScoped<IRecurringJobService, RecurringJobService>();
            builder.Services.AddTransient<IGithubService, GithubRepoServices>();
            builder.Services.AddTransient<IBlogPostService, BlogPostService>();
            builder.Services.AddTransient<ITwitchService, TwitchService>();
            builder.Services.AddTransient<IYouTubeService, YouTubeService>();
            return builder;
        }

        public static IFunctionsHostBuilder ConfigureMessagingHandlers(this IFunctionsHostBuilder builder)
        {
            var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            builder.Services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(config.GetConnectionString("ServiceBusConnectionString"));
            });
            builder.Services.AddScoped<HandlerResolver>(sp => message =>
              {
                  switch (message)
                  {
                      case MessageType.GithubRepoUpdate:
                          return sp.GetService<GithubMessageHandler>();
                      case MessageType.UserBlogPostsUpdate:
                          return sp.GetService<BlogPostsMessageHandler>();
                      case MessageType.VideoPlatformUpdate:
                          return sp.GetService<VideoPlatformMessageHandler>();
                      case MessageType.TwitchWebhookSubscription:
                          return sp.GetService<TwitchWebhookSubscriptionHandler>();
                      default:
                          return null;

                  }
              });
            builder.Services.AddScoped<GithubMessageHandler>();
            builder.Services.AddScoped<BlogPostsMessageHandler>();
            builder.Services.AddScoped<VideoPlatformMessageHandler>();
            builder.Services.AddScoped<TwitchWebhookSubscriptionHandler>();
            builder.Services.AddScoped<IRecurringJobService, RecurringJobService>();
            builder.Services.AddTransient<IMessageHandlerFactory, MessageHandlerFactory>();
            builder.Services.AddTransient<IGithubService, GithubRepoServices>();
            builder.Services.AddTransient<IBlogPostService, BlogPostService>();
            builder.Services.AddTransient<ITwitchService, TwitchService>();
            builder.Services.AddTransient<IYouTubeService, YouTubeService>();
            return builder;
        }

        public static IFunctionsConfigurationBuilder ConfigureKeyVault(this IFunctionsConfigurationBuilder builder, string vaultUrl, Assembly assembly = null)
        {
            builder.ConfigurationBuilder
                .AddAzureKeyVault(new Uri(vaultUrl), new DefaultAzureCredential());
            if (assembly != null)
            {
                builder.ConfigurationBuilder.AddUserSecrets(assembly);
            }

            return builder;
        }
    }
}
