using Rabbit.IOC;
using Rabbit.IWasThere.Common;
using Raven.Abstractions.Extensions;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Packaging;
using System;
using System.Data.Entity.Migrations;
using System.Reflection;
using System.Web.Mvc;

namespace Rabbit.iOnline.Ioc.SimpleInjector
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
            var container = RegisterPackages(dbSystem);

            // Controllers
            container.RegisterMvcControllers(controllersAssembly);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static Container RegisterPackages(string dbSystem)
        {
            var container = new Container();

            ModuleHelper.GetModuleTypes(typeof(SystemConfig).Assembly)
                .CreateModules()
                .FilterWith(dbSystem)
                .ForEach(x => ((IPackage)x).RegisterServices(container));

            return container;
        }
    }
}
