using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stone.Application.Interfaces;
using Stone.Application.Services;
using Stone.Common.Interfaces;
using Stone.Domain.Interfaces;
using Stone.Domain.Repositories;
using Stone.Infra.Http;
using Stone.Infra.Providers;
using Stone.Schedulers;
using System;
using System.Data.Entity;

namespace Stone.Config
{
    public class ContainerConfig
    {
        public static IServiceProvider Configure(IServiceCollection services, IConfigurationRoot configuration)
        {
            var builder = new ContainerBuilder();

            var connectionString = configuration.GetSection("ConnectionString").Value;

            builder.RegisterType<CityApplicationService>().As<ICityApplicationService>().InstancePerLifetimeScope();
            builder.RegisterType<TemperatureApplicationService>().As<ITemperatureApplicationService>().InstancePerLifetimeScope();
            builder.RegisterType<Repository>().As<IRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HangfireScheduler>().As<IJobRecurring>().SingleInstance();
            builder.RegisterType<JobApplicationService>().As<IJobApplicationService>().SingleInstance();
            builder.RegisterType<EFProvider>().As<IDataProvider>().InstancePerLifetimeScope();
            builder.RegisterType<HttpAgent>().As<IHttpAgent>().InstancePerLifetimeScope();
            builder.Register(c => { return new EFContext(connectionString); }).As<DbContext>().InstancePerLifetimeScope();

            builder.Populate(services);

            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
