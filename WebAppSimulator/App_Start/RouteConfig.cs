using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAppSimulator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute("display", "display/{ip}/{port}",
            defaults: new { controller = "First", action = "display"});

            /*routes.MapRoute("display", "display/{ip}/{port}/{times_per_second}",
            defaults: new { controller = "First", action = "display" });*/

            routes.MapRoute("save", "save/{ip}/{port}/{times_per_second}/{seconds}/{file_name}",
            defaults: new { controller = "First", action = "save" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "First", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
