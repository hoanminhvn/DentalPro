using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Web_ProductWithQRCode
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "api/{controller}"
            );

            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "all" }
            );
            config.Routes.MapHttpRoute(
                name: "ControllerAndId",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ControllerAndActionAndId",
                routeTemplate: "api/{controller}/{action}/{id}"
            );
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
