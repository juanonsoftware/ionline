using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IOC;
using Rabbit.IWasThere.Common;
using Raven.Abstractions.Extensions;
using System;
using System.Data.Entity.Migrations;
using System.Reflection;
using System.Web.Mvc;
using IModule = Autofac.Core.IModule;

namespace Rabbit.iOnline.Ioc.Autofac
{
    public static class SystemConfig
    {
        public static void ConfigDatabase(string dbSystem)
        {
            if (GlobalConstants.SqlServer.Equals(dbSystem, StringComparison.InvariantCultureIgnoreCase))
            {
                new DbMigrator(new IWasThere.Data.EF.Migrations.Configuration()).Update();
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
            }
        }

        public static void ConfigDependencyContainer(string dbSystem, Assembly controllersAssembly)
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(controllersAssembly);

            // Register all modules
            ModuleHelper.GetModuleTypes(typeof(SystemConfig).Assembly)
                .CreateModules()
                .FilterWith(dbSystem)
                .ForEach(x => builder.RegisterModule((IModule)x));

            builder.RegisterFilterProvider();

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}