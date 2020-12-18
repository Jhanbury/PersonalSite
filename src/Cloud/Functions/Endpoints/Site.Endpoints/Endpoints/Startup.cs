using System;
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
