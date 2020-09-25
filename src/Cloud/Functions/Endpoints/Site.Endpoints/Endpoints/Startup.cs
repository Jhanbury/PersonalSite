using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
            builder.Services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly, typeof(InfrastructureProfile).GetTypeInfo().Assembly });
            builder.Services.AddTransient<IGithubService, GithubRepoServices>();
            builder.Services.AddTransient<IBlogPostService, BlogPostService>();
            builder.Services.AddTransient<ITwitchService, TwitchService>();
            builder.Services.AddTransient<IYouTubeService, YouTubeService>();
            
        }
    }
}