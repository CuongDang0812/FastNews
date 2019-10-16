using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastNews
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Post Detail",
                url: "{metaTitleCategory}/{metaTitlePost}-{postID}",
                defaults: new { controller = "Post", action = "PostDetail", id = UrlParameter.Optional },
                namespaces: new[] { "FastNews.Controllers" }
            );

            routes.MapRoute(
                name: "Category Detail",
                url: "{metaTitleCategory}/{categoryID}",
                defaults: new { controller = "Category", action = "CategoryDetail", id = UrlParameter.Optional },
                namespaces: new[] { "FastNews.Controllers" }
            );

            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                    ,
                    namespaces: new[] { "FastNews.Controllers" }
                );
        }
    }
}
