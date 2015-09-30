using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using Rabbit.IOC;
using Rabbit.IWasThere.Common;
using Raven.Abstractions.Extensions;
using System;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Reflection;
using System.Web.Mvc;
using IModule = Autofac.Core.IModule;

namespace Rabbit.iOnline.Ioc.Autofac
{
    public static class SystemConfig
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SystemConfig));

        public static void ConfigDatabase()
        {
            var dbSystem = ConfigurationManager.AppSettings[GlobalConstants.DatabaseSystem];
            if (GlobalConstants.SqlServer.Equals(dbSystem, StringComparison.InvariantCultureIgnoreCase))
            {
                new DbMigrator(new IWasThere.Data.EF.Migrations.Configuration()).Update();
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
            }
        }

        public static void ConfigDependencyContainer(Assembly controllersAssembly)
        {
            var dbSystem = ConfigurationManager.AppSettings[GlobalConstants.DatabaseSystem];
            Logger.InfoFormat("DatabaseSystem: {0}", dbSystem);

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