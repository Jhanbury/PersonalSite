using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
using FluentCache;
using FluentCache.Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using Serilog.Events;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;
using Site.Application.Infrastructure;
using Site.Application.Infrastructure.AutoMapper;
using Site.Application.Interfaces;
using Site.Application.Messaging;
using Site.Infrastructure;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Messages;
using Site.Infrastructure.Modules;
using Site.Infrastructure.Services;
using Site.Persistance;
using Site.Persistance.Repository;

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
            ConfigureCaching(services, dbConnectionString);
            services.AddHttpClient();
            ConfigureAutoMapper(services);
            ConfigureMediatR(services);
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
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterModule<ApplicationModule>();
            ApplicationContainer = containerBuilder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
            //var sqlStorage = new SqlServerStorage(connectionString);
            //sqlStorage.UseServiceBusQueues(serviceBusConnectionString, "critical", "default");
            //RecurringJob.AddOrUpdate<IRecurringJobService>(service => service.UpdateGithubRepos(1,"Jhanbury"),Cron.Minutely);
            //Log.Logger.Information("Test Log from Startup");
            //var serviceBusConnectionString = Configuration["ServiceBusConnectionString"];
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        // base-address of your identityserver
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;
            //        // name of the API resource
            //        options.Audience = "api1";
            //    });
        }

        
        private void ConfigureCaching(IServiceCollection services, string connectionString)
        {

            services.AddDistributedMemoryCache();
            services.AddSingleton<ICache>(sp =>
                new FluentIDistributedCache(sp.GetService<IDistributedCache>(), new Serializer()));
            services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
            
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
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
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
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
            BackgroundJob.Enqueue<IRecurringJobService>(service => service.UpdateGithubRepos(1, "JHanbury"));
            //RecurringJob.AddOrUpdate<IBlogPostService>(service => service.UpdateBlogPostsForUser(1),);
            //RecurringJob.AddOrUpdate<IRecurringJobService>(service => service.UpdateGithubRepos(1, "JHanbury"), Cron.Minutely);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseCors("cors");
            app.UseHttpsRedirection();
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            //app.UseAuthentication();
            app.UseMvc();
        }
    }
}
