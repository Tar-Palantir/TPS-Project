using System.Web.Mvc;
using System.Web.Routing;

namespace TPS.WeiXin.Entrance.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "WeiXinReceive",
                url: "ReceiveMain/{accountID}/",
                defaults: new { controller = "Receive", action = "Main" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
