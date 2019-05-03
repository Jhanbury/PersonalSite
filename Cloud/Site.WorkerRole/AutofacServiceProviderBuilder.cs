using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Site.Application.Infrastructure.AutoMapper;
using Site.Infrastructure.Modules;
using Site.Persistance;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace Site.WorkerRole
{
    public class AutofacServiceProviderBuilder : IServiceProviderBuilder
    {
        private readonly IConfiguration _configuration;

        public AutofacServiceProviderBuilder(IConfiguration configuration) => _configuration = configuration;

        public IServiceProvider Build()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var builder = new ContainerBuilder();
            builder.Populate(services); // Populate is needed to have support for scopes.
            builder.RegisterModule<ApplicationModule>();
            return new AutofacServiceProvider(builder.Build());
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["EFConnectionString"];
            //var logger = ConfigureSerilog(connectionString);
            //logger.Information("This is from Worker");
            services.AddLogging(builder => { builder.AddSerilog(); });
            //services.AddScoped<Microsoft.Extensions.Logging.ILogger>(provider => logger);
            services.AddHttpClient();
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
            //services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));
        }
    }
}