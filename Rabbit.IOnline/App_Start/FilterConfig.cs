using Rabbit.Web.Mvc.Filters;
using System.Web.Mvc;

namespace Rabbit.IOnline.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequireHttpsInProductionAttribute(new XForwardedProtoHandleNonHttpsWorker()));
        }
    }
}