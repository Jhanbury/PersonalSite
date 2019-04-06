using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Personal_Site_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, builder) =>
                    {
                        try
                        {
                            var env = ctx.HostingEnvironment;

                            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                            var vaultUrl = builder.Build().GetSection("keyvault")?.Value;
                            var keyVaultEndpoint = vaultUrl;
                            if (!string.IsNullOrEmpty(keyVaultEndpoint))
                            {
                                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                                var keyVaultClient = new KeyVaultClient(
                                    new KeyVaultClient.AuthenticationCallback(
                                        azureServiceTokenProvider.KeyVaultTokenCallback));
                                builder.AddAzureKeyVault(
                                    keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                        }

                    }
                )
                .UseSerilog()
                .UseStartup<Startup>();
        }
           
    }
}
