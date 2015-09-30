using log4net;
using Rabbit.IOC;
using Rabbit.IWasThere.Common;
using Raven.Abstractions.Extensions;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Packaging;
using System;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Reflection;
using System.Web.Mvc;

namespace Rabbit.iOnline.Ioc.SimpleInjector
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
            var container = RegisterPackages();

            // Controllers
            container.RegisterMvcControllers(controllersAssembly);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static Container RegisterPackages()
        {
            var dbSystem = ConfigurationManager.AppSettings[GlobalConstants.DatabaseSystem];
            Logger.InfoFormat("DatabaseSystem: {0}", dbSystem);

            var container = new Container();

            ModuleHelper.GetModuleTypes(typeof(SystemConfig).Assembly)
                .CreateModules()
                .FilterWith(dbSystem)
                .ForEach(x => ((IPackage)x).RegisterServices(container));

            return container;
        }
    }
}