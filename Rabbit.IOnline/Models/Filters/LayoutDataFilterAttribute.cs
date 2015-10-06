using System.Web.Mvc;
using Rabbit.Configuration;

namespace Rabbit.IOnline.Models.Filters
{
    public class LayoutDataFilterAttribute : ActionFilterAttribute
    {
        private IConfiguration Configuration
        {
            get
            {
                return DependencyResolver.Current.GetService<IConfiguration>();
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.AdFileUri = Configuration.Get("AdFileUri");
            base.OnActionExecuting(filterContext);
        }
    }
}
