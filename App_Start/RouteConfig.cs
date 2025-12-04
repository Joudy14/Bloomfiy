using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bloomfiy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Bloomfiy.Controllers" }  // Specify namespace


            );

            routes.MapRoute(
                name: "ProductByCategory",
                url: "products/category/{categoryId}",
                defaults: new { controller = "Product", action = "ByCategory" }
            );

            routes.MapRoute(
                name: "ProductSearch",
                url: "products/search/{query}",
                defaults: new { controller = "Product", action = "Search" }
            );
        }
    }


}