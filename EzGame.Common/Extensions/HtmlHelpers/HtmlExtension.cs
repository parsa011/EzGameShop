using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace EzGame.Common.Extensions.HtmlHelpers
{
    public static class HtmlExtension
    {
        /// <summary>
        /// for add active class to a element aht is active
        /// </summary>
        /// <param name="htmlHelper" type="extension for IHtmlHelper"></param>
        /// <param name="controller" type="string"></param>
        /// <returns> active string </returns>
        public static string IsActive(this IHtmlHelper htmlHelper, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController);

            return returnActive ? "active" : "";
        }

        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && (action == routeAction || routeAction == "Details"));

            return returnActive ? "active" : "";
        }

        public static string IsActive(this IHtmlHelper htmlHelper, string title, bool asTitle)
        {
            var routeData = new HttpContextAccessor();

            if (routeData.HttpContext.GetRouteValue("title") != null)
            {
                var routeTitle = routeData.HttpContext.GetRouteValue("title").ToString().MakeUrlString();
                var returnActive = (title == routeTitle);

                return returnActive ? "active" : "";
            }

            return "";
        }

        
        public static string IsThisView(this IHtmlHelper htmlHelper,string url)
        {
            return htmlHelper.ViewContext.View.Path.ToLower() == url ? "active" : "";
        }
    }
}