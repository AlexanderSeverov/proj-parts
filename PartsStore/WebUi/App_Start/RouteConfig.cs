using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               null,
               "",
               new { controller = "Parts", action = "List",cat=(string)null ,page = 1 }
               );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Parts", action = "List",cat = (string)null },
                constraints: new { page = @"\d+" }
                );

            routes.MapRoute(
                null,
                "{cat}",
                new { controller = "Parts", action = "List",page = 1 }
                );

            routes.MapRoute(
                null,
                "{cat}/Page{page}",
                new { controller = "Parts", action = "List" },
                new {page = @"\d+"}
                );

            routes.MapRoute(
                null,
                "{controller}/{action}"
                );
        }
    }
}
