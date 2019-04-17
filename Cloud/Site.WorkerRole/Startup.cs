using System.Reflection;
using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Site.Application.Infrastructure.AutoMapper;
using Site.Application.Interfaces;
using Site.Infrastructure.Services;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;
using Site.Persistance;
using Site.Persistance.Repository;
using Site.WorkerRole;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Site.WorkerRole
{
    internal class Startup : IWebJobsStartup
    {
        public IConfiguration Configuration { get; set; }
        public void Configure(IWebJobsBuilder builder)
        {
            
            builder.AddAzureStorageCoreServices();
            builder.AddServiceBus();
            var tempProvider = builder.Services.BuildServiceProvider();
            var config = tempProvider.GetRequiredService<IConfiguration>();
            Configuration = config;
            //builder.AddAzureKeyVault(config["AzureKeyVault_Uri"]);
            builder.AddDependencyInjection(ConfigureServices);
        }
            

        private void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["EFConnectionString"];
            var logger = ConfigureSerilog(connectionString);
            //logger.Information("This is from Worker");
            services.AddLogging(builder => { builder.AddSerilog(); });
            //services.AddScoped<Microsoft.Extensions.Logging.ILogger>(provider => logger);
            services.AddHttpClient();
            services.AddTransient<IGithubService, GithubRepoServices>();
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));
        }

        public ILogger ConfigureSerilog(string connectionString)
        {
            return Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Azure Function Worker")
                .WriteTo.MSSqlServer(connectionString, "Logs")
                .CreateLogger();
            
        }
    }
}