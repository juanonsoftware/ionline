using log4net;
using Rabbit.iOnline.Ioc.SimpleInjector.Packages;
using Rabbit.IWasThere.Common;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
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
            var container = new Container();

            new DataServicePackage().RegisterServices(container);

            var dbSystem = ConfigurationManager.AppSettings[GlobalConstants.DatabaseSystem];
            Logger.InfoFormat("DatabaseSystem: {0}", dbSystem);

            if (GlobalConstants.RavenDb.Equals(dbSystem, StringComparison.InvariantCultureIgnoreCase))
            {
                new RavenDbDataPackage().RegisterServices(container);
            }
            else if (GlobalConstants.DocumentDb.Equals(dbSystem, StringComparison.InvariantCultureIgnoreCase))
            {
                new AzureDocumentDbPackage().RegisterServices(container);
            }
            else
            {
                new EfDataPackage().RegisterServices(container);
            }

            return container;
        }
    }
}