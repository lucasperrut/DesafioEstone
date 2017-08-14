using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stone.Config;
using Stone.Middleware;
using Hangfire;
using Hangfire.MemoryStorage;
using Stone.Schedulers;
using Stone.Domain.Repositories;
using Stone.Domain.Entities;
using System.Threading.Tasks;
using Stone.Infra.Providers;
using Stone.Common.Interfaces;
using Stone.Domain.Interfaces;
using Stone.Application.Interfaces;

namespace Stone
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddHangfire(x => x.UseStorage(new MemoryStorage()));

            return ContainerConfig.Configure(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IJobRecurring job, IJobApplicationService jobService)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
            var interval = Convert.ToInt32(Configuration.GetSection("JobInterval").Value);
            job.Enqueue(() => jobService.CreateTemperature(), interval);

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RouteMiddleware>();

            app.UseMvc();
        }
    }
}
