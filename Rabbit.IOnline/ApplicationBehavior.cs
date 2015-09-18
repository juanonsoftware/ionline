using Rabbit.iOnline.Ioc;
using Rabbit.IOnline.App_Start;
using Rabbit.IWasThere.Common;
using Rabbit.SerializationMaster;
using Rabbit.SerializationMaster.ServiceStack;
using Rabbit.Web.Mvc;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Rabbit.IOnline
{
    public class ApplicationBehavior : DefaultMvcHttpApplicationBehavior
    {
        private readonly IApplicationBootstrap _applicationBootstrap;

        public ApplicationBehavior(IApplicationBootstrap applicationBootstrap)
        {
            _applicationBootstrap = applicationBootstrap;
        }

        protected override void OnAfterStart()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            base.OnAfterStart();
        }

        protected override void OnBeforeStart()
        {
            base.OnBeforeStart();

            SerializationContext.Current.Initialize(new JsonSerializationStrategy());

            _applicationBootstrap.Initialize(new Dictionary<string, object>()
            {
                {GlobalConstants.ControllersAssembly, typeof (MvcApplication).Assembly}
            });
        }
    }
}