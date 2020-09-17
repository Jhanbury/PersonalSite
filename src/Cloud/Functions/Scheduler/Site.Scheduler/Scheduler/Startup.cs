using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Site.Application.Interfaces;
using Site.Infrastructure;
using Site.Infrastructure.Services;
using Site.Persistance;
using Site.Persistance.Repository;

[assembly:FunctionsStartup(typeof(Scheduler.Startup))]
namespace Scheduler
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var env = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
            if (!string.IsNullOrEmpty(env) && env.Equals("Development"))
            {
                var context = builder.Services.BuildServiceProvider().GetService<IOptions<ExecutionContextOptions>>().Value;
                var localConfig = new ConfigurationBuilder()
                    .SetBasePath(context.AppDirectory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets(typeof(Startup).Assembly)
                    .AddEnvironmentVariables()
                    .Build();
                var uri = localConfig.GetValue<string>("VaultUri");
                builder.ConfigureKeyVault(uri);
            }

            var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = config["ConnectionString"];
            builder.ConfigureSchedulerServices(connectionString);
            
            
        }
    }
}