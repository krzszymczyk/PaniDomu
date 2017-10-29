using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PaniDomu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Expenses",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Expenses", action = "Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "CreatingExpenses",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Expenses", action = "ShowExpenses", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DisplayingExpensesByDates",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Expenses", action = "ShowExpensesByDates", id = UrlParameter.Optional }
            );
            
        }
    }
}
