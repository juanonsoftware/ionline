using System.Web.Mvc;
using Rabbit.IOnline.Models;

namespace Rabbit.IOnline.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequireHttpsOnProductionAttribute());
        }
    }
}