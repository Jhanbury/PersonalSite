using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Site.Infrastructure;

[assembly: FunctionsStartup(typeof(Endpoints.Startup))]
namespace Endpoints
{
    public class Startup : FunctionsStartup
  {

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
      var env = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
      if (!string.IsNullOrEmpty(env) && env.Equals("Development"))
      {
        var localConfig = new ConfigurationBuilder().AddUserSecrets(typeof(Startup).Assembly)
          .AddEnvironmentVariables()
          .Build();
        var uri = localConfig.GetValue<string>("VaultUri");
        builder.ConfigureKeyVault(uri);
      }
    }
    public override void Configure(IFunctionsHostBuilder builder) =>
      builder
        .ConfigureSerilog()
        .ConfigureServices()
        .ConfigureMessagingHandlers();

    
  }
}
