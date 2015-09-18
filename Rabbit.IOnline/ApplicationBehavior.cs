using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Rabbit.IOnline.App_Start;
using Rabbit.Web.Mvc;

namespace Rabbit.IOnline
{
    public class ApplicationBehavior : DefaultMvcHttpApplicationBehavior
    {
        protected override void OnAfterStart()
        {
            base.OnAfterStart();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override void OnBeforeStart()
        {
            SystemConfig.ConfigDatabase();
            SystemConfig.ConfigureSerialization();
            SystemConfig.ConfigDependencyContainer();

            base.OnBeforeStart();
        }
    }
}