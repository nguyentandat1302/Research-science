using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Research_science
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "Default",
                url: "{controller}/{action}/{id}",
                 defaults: new { controller = "HomePage", action = "Index", id = UrlParameter.Optional }
                     , namespaces: new[] { "Research science.Controller" }
             );



            routes.MapRoute(
                  name: "Employer_Default",
                 url: "Employer/{controller}/{action}/{id}",
                 defaults: new { controller = "Employer", action = "Menu", id = UrlParameter.Optional }
                     , namespaces: new[] { "Research science.Controller" });

            //routes.MapRoute(
            //     name: "Admin",
            //    url: "Admin/{controller}/{action}/{id}",
            //     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //         , namespaces: new[] { "Research science.Areas.Admin.Controllers" }
            // );




        }
    }
}
