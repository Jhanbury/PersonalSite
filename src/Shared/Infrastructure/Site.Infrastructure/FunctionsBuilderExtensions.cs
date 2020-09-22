using System;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Site.Application.Infrastructure.AutoMapper;
using Site.Application.Interfaces;
using Site.Application.Interfaces.Messaging;
using Site.Application.Users.Queries;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Modules;
using Site.Infrastructure.Services;
using Site.Persistance;
using Site.Persistance.Repository;

namespace Site.Infrastructure
{
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


        public static IFunctionsHostBuilder ConfigureSchedulerServices(this IFunctionsHostBuilder builder, string connectionString)
        {
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

        public static IFunctionsHostBuilder ConfigureKeyVault(this IFunctionsHostBuilder builder, string vaultUrl, Assembly assembly = null)
        {

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    azureServiceTokenProvider.KeyVaultTokenCallback));

            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddAzureKeyVault(vaultUrl, keyVaultClient, new DefaultKeyVaultSecretManager());
            if (assembly != null)
            {
                configBuilder.AddUserSecrets(assembly);
            }


            builder.Services.AddSingleton<IConfiguration>(configBuilder.Build());
            return builder;
        }
    }
}