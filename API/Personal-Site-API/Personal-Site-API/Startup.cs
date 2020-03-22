using System;
using System.Reflection;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using FluentCache;
using FluentCache.Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;
using Site.Application.Infrastructure;
using Site.Application.Infrastructure.AutoMapper;
using Site.Infrastructure;
using Site.Application.Interfaces;
using Site.Persistance;

namespace Personal_Site_API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    public IContainer ApplicationContainer { get; set; }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      var dbConnectionString = Configuration["ConnectionString"];
      var devToAPIKey = Configuration["DevtoAPIKey"];
      ConfigureCaching(services, dbConnectionString);
      ConfigureHttpSettings(services);
      ConfigureAutoMapper(services);
      ConfigureMediatR(services);
      ConfigureHttpClientFactory(services, devToAPIKey);
      ConfigureSerilog(dbConnectionString);
      ConfigureHangFire(services, dbConnectionString);
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddCors(options =>
      {
        options.AddPolicy("cors",
                  builder =>
                  {
                builder.AllowAnyHeader()
                          .AllowAnyOrigin()
                          .AllowAnyMethod();
              });
      });
      
      return new Container().WithDependencyInjectionAdapter(services)
        .ConfigureServiceProvider<CompositionRoot>();

    }

    private void ConfigureHttpSettings(IServiceCollection services)
    {
      services.AddHttpClient();
      services.AddHttpContextAccessor();
    }

    private void ConfigureCaching(IServiceCollection services, string connectionString)
    {
      services.AddDistributedMemoryCache();
      services.AddSingleton<ICache>(sp =>
          new FluentIDistributedCache(sp.GetService<IDistributedCache>(), new Serializer()));
      services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
    }

    private void ConfigureHttpClientFactory(IServiceCollection services, string blogAPIKey)
    {
      services.AddHttpClient("dev.to", client =>
      {
        client.BaseAddress = new Uri("https://dev.to/");
        client.DefaultRequestHeaders.Add("api-key", blogAPIKey);

      });
      services.AddHttpClient("github", client =>
      {
        client.BaseAddress = new Uri("https://api.github.com");
        client.DefaultRequestHeaders.Add("User-Agent", "Personal-Site");
      });
    }

    private void ConfigureAutoMapper(IServiceCollection services)
    {
      services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly, typeof(InfrastructureProfile).GetTypeInfo().Assembly });
    }

    public void ConfigureMediatR(IServiceCollection services)
    {
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

      services.AddMediatR(typeof(GetAllGithubReposQuery).GetTypeInfo().Assembly);
    }

    public void ConfigureSerilog(string connectionString)
    {
      Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Information()
          .Enrich.FromLogContext()
          .Enrich.WithProperty("Application", "API")
          .WriteTo.MSSqlServer(connectionString, "Logs")
          .CreateLogger();
    }

    public void ConfigureHangFire(IServiceCollection services, string connectionString)
    {
      services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
      services.AddHangfireServer();
      JobStorage.Current = new SqlServerStorage(connectionString);

      RecurringJob.AddOrUpdate<IRecurringJobService>(service => service.UpdateGithubRepos(1, "JHanbury"), Cron.Daily);
      RecurringJob.AddOrUpdate<IRecurringJobService>(service => service.UpdateUserBlogs(1), Cron.Daily);
      RecurringJob.AddOrUpdate<IRecurringJobService>(service => service.UpdateVideoPlatforms(1), Cron.Daily);
      RecurringJob.AddOrUpdate<IRecurringJobService>(service => service.SubscribeToTwitchWebhooks(1), Cron.Daily);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }


      app.UseCors("cors");
      app.UseHttpsRedirection();
      app.UseHangfireServer();
      app.UseHangfireDashboard();
      app.UseMvc();
    }
  }
}
