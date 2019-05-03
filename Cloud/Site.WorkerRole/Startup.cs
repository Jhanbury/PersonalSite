using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;
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
            //builder.AddAzureKeyVault(config["AzureKeyVault_Uri"]);
            builder.AddDependencyInjection<AutofacServiceProviderBuilder>();
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