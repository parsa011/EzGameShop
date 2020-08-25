using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.AspNetCore.Http;

namespace EzGame.Common.Filters.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class NotAuthorizeAttribute : ActionFilterAttribute
    {
        public NotAuthorizeAttribute(string returnUrl = "/")
        {
            this.ReturnUrl = returnUrl;
        }

        private string ReturnUrl { get; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            HttpContext httpContext = new DefaultHttpContext();
            if (httpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.Response.Redirect(ReturnUrl);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}