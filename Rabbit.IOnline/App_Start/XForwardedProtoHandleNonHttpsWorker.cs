﻿using Rabbit.Web.Mvc.Filters;
using System;
using System.Web.Mvc;

namespace Rabbit.IOnline.App_Start
{
    public class XForwardedProtoHandleNonHttpsWorker : HandleNonHttpsWorker
    {
        public override bool CanValidate(AuthorizationContext filterContext)
        {
            if ("https".Equals(filterContext.HttpContext.Request.Headers["X-Forwarded-Proto"], StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return base.CanValidate(filterContext);
        }
    }
}