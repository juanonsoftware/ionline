using Rabbit.IOnline.App_Start;
using Rabbit.IOnline.Models;
using Rabbit.IWasThere.Data.EF;
using Rabbit.SerializationMaster;
using Rabbit.SerializationMaster.ServiceStack;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Configuration = Rabbit.IWasThere.Data.EF.Migrations.Configuration;

namespace Rabbit.IOnline
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeDatabase();
            ConfigureSerialization();
        }

        private void InitializeDatabase()
        {
            if (string.Equals(ConfigurationManager.AppSettings["UseMigration"], "true",
                StringComparison.InvariantCultureIgnoreCase))
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
            }
            else
            {
                Database.SetInitializer(new SystemDbInitializer());
            }
        }

        private void ConfigureSerialization()
        {
            SerializationContext.Current.Initialize(new JsonSerializationStrategy());
        }
    }
}