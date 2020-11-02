using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryDeweyApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "MyCustomRoute",
               url: "ReplacingBooks/Result/{action}",
               defaults: new { controller = "ReplacingBooksResult", action = "Index"}
           );

            routes.MapRoute(
                name: "IdentifyCustomRoute",
                url: "IdentifyAreas/Result/{action}",
                defaults: new { controller = "IdentifyAreasResult", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
