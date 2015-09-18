using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using Rabbit.iOnline.Ioc.Autofac.Modules;
using Rabbit.IWasThere.Common;
using System;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Reflection;
using System.Web.Mvc;

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
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(controllersAssembly);

            builder.RegisterModule(new DataServiceModule());

            var dbSystem = ConfigurationManager.AppSettings[GlobalConstants.DatabaseSystem];
            Logger.InfoFormat("DatabaseSystem: {0}", dbSystem);

            if (GlobalConstants.RavenDb.Equals(dbSystem, StringComparison.InvariantCultureIgnoreCase))
            {
                builder.RegisterModule(new RavenDbDataModule());
            }
            else if (GlobalConstants.DocumentDb.Equals(dbSystem, StringComparison.InvariantCultureIgnoreCase))
            {
                builder.RegisterModule(new AzureDocumentDbDataModule());
            }
            else
            {
                builder.RegisterModule(new EfDataModule());
            }

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}