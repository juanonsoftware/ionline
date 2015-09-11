using Rabbit.IOnline.App_Start;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Rabbit.IOnline
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SystemConfig.ConfigDatabase();
            SystemConfig.ConfigureSerialization();
            SystemConfig.ConfigDependencyContainer();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}