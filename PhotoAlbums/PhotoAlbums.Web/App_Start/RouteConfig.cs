﻿using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoAlbums.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Albums", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
