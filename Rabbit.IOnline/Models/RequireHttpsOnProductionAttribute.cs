using System;
using System.Linq;
using System.Web.Mvc;

namespace Rabbit.IOnline.Models
{
    public class RequireHttpsOnProductionAttribute : RequireHttpsAttribute
    {
        private readonly string[] _excludeHosts;

        public RequireHttpsOnProductionAttribute(params string[] excludeHosts)
        {
            _excludeHosts = excludeHosts;
        }

        /// <summary>
        /// This property is False by default
        /// </summary>
        public bool RequireHttpsLocally
        {
            get;
            set;
        }

        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            if (!RequireHttpsLocally && filterContext.HttpContext.Request.IsLocal)
            {
                return;
            }

            var url = filterContext.HttpContext.Request.Url;
            if (url != null && _excludeHosts.Contains(url.Host, StringComparer.InvariantCultureIgnoreCase))
            {
                return;
            }

            base.HandleNonHttpsRequest(filterContext);
        }
    }
}