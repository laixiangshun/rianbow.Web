using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace rainbow.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Globalization",
                "{lang}/{controller}/{action}/{id}",
                new { lang = "zh", controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { lang = "^[a-zA-Z]{2}(-[a-zA-Z]{2})?$" } //参数约束
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
