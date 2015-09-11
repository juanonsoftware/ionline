using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IOnline.App_Start.Autofac;
using Rabbit.IWasThere.Common;
using Rabbit.SerializationMaster;
using Rabbit.SerializationMaster.ServiceStack;
using System;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using Configuration = Rabbit.IWasThere.Data.EF.Migrations.Configuration;

namespace Rabbit.IOnline.App_Start
{
    public static class SystemConfig
    {
        public static void ConfigureSerialization()
        {
            SerializationContext.Current.Initialize(new JsonSerializationStrategy());
        }

        public static void ConfigDatabaseMigration()
        {
            new DbMigrator(new Configuration()).Update();
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        public static void ConfigDependencyContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new DataServiceModule());

            var dbSystem = ConfigurationManager.AppSettings[Constants.DatabaseSystem];
            if (Constants.RavenDb.Equals(dbSystem, StringComparison.InvariantCultureIgnoreCase))
            {
                builder.RegisterModule(new RavenDbDataModule());
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