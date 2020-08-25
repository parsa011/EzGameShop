using System;
using EzGame.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EzGame.Common.Filters.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.IsAjax())
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}