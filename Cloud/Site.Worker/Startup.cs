using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Site.Application.Infrastructure.AutoMapper;
using Site.Application.Interfaces;
using Site.Infrastructure.Modules;
using Site.Persistance;
using Site.Persistance.Repository;
using Site.Worker;
using System;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Site.Application.Interfaces.Messaging;
using Site.Infrastructure;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Site.Worker
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
                .Build();
            //var services = new ServiceCollection();
            builder.Services.AddHttpClient();
            //builder.useServiceProviderFactory
            var connectionString = config["DBConnectionString"];
            ////var logger = ConfigureSerilog(connectionString);
            ////logger.Information("This is from Worker");
            //var autoFacBuilder = new ContainerBuilder();
            // Populate is needed to have support for scopes.

            //builder.Services.AddLogging(b => { b.Services.AddSerilog(); });
            //services.AddScoped<Microsoft.Extensions.Logging.ILogger>(provider => logger);
            //services.AddHttpClient();
            //builder.Services.AddAutofac(autoFacBuilder =>
            //{
            //    autoFacBuilder.Populate(builder.Services);
            //    autoFacBuilder.RegisterModule<ApplicationModule>();
            //    //autoFacBuilder.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            //});
            builder.Services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            builder.Services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));
            //builder.servicesRegisterType<GithubRepoServices>().As<IGithubService>();
            //builder.RegisterType<BlogPostService>().As<IBlogPostService>();
            //builder.RegisterType<RecurringJobService>().As<IRecurringJobService>();
            //builder.RegisterType<GithubRepoServices>().As<IGithubService>();
            //builder.RegisterGeneric(typeof(EFRepository<,>)).As(typeof(IRepository<,>));
            //builder.RegisterType<GithubMessageHandler>().Keyed<IMessageHandler<IMessage>>(MessageType.GithubRepoUpdate);
            //builder.RegisterType<BlogPostsMessageHandler>().Keyed<IMessageHandler<IMessage>>(MessageType.UserBlogPostsUpdate);
            
            builder.Services.AddTransient<IGithubService, GithubRepoServices>();
            builder.Services.AddTransient<IBlogPostService, BlogPostService>();
            builder.Services.AddTransient<GithubMessageHandler>();
            //builder.Services.AddTransient<GithubMessageHandler>();
            builder.Services.AddTransient<IMessageHandlerFactory,MessageHandlerFactory>();




        }

        
    }
}