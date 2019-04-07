using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Azure.ServiceBusQueue;
using Hangfire.SqlServer;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Hangfire.Common;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Events;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;
using Site.Application.Infrastructure;
using Site.Application.Infrastructure.AutoMapper;
using Site.Application.Interfaces;
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString"];
            ConfigureApplicationServices(services, connectionString);
            services.AddHttpClient();
            ConfigureAutoMapper(services);
            ConfigureMediatR(services);
            ConfigureSerilog(connectionString);
            ConfigureHangFire(services, connectionString);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


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

        private void ConfigureApplicationServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IGithubService, GithubRepoServices>();
            services.AddScoped<IRecurringJobService, RecurringJobService>();
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));
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
            RecurringJob.AddOrUpdate<IGithubService>(service => service.UpdateGithubReposForUser(1, "JHanbury"), Cron.Minutely);
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

            app.UseHttpsRedirection();
            app.UseHangfireServer();
            //app.UseAuthentication();
            app.UseMvc();
        }
    }
}
