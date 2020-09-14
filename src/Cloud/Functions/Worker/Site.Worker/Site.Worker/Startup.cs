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
using Site.Infrastructure;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Modules;
using Site.Infrastructure.Services;
using Site.Persistance;
using Site.Persistance.Repository;

[assembly: FunctionsStartup(typeof(Site.Worker.Startup))]
namespace Site.Worker
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var localConfig = GetLocalConfiguration();
            var uri = localConfig.GetValue<string>("VaultUri");
            builder
                .ConfigureKeyVault(uri)
                .ConfigureServices()
                .ConfigureMessagingHandlers();

        }

        private static IConfiguration GetLocalConfiguration()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets(typeof(Startup).Assembly)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}