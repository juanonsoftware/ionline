using System;
using Rabbit.Web.Mvc.Filters;
using System.Web.Mvc;

namespace Rabbit.IOnline.App_Start
{
    public class AppHarborHttpsInProductionAttribute : RequireHttpsInProductionAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsSecureConnection)
            {
                return;
            }

            if ("https".Equals(filterContext.HttpContext.Request.Headers["X-Forwarded-Proto"], StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AppHarborHttpsInProductionAttribute());
        }
    }
}