using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web_ProductWithQRCode
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("fonts*.woff");
            routes.IgnoreRoute("*.js");
            routes.IgnoreRoute("*.html");
            routes.IgnoreRoute("*.css");
            routes.IgnoreRoute("api/*");
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Templates", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Logout",
                url: "logout",
                defaults: new { controller = "Templates", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ResetPass",
                url: "reset",
                defaults: new { controller = "Admin", action = "ResetPass", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ChangePass",
                url: "change-pass",
                defaults: new { controller = "Admin", action = "ChangePass", id = UrlParameter.Optional }
            );
            
            //Admin area
            routes.MapRoute(
                name: "Admin",
                url: "admin",
                defaults: new { controller = "Admin", action = "Index", path = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminPage",
                url: "admin/{path}",
                defaults: new { controller = "Admin", action = "Index", path = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminPageWithid",
                url: "admin/{path}/{id}",
                defaults: new { controller = "Admin", action = "Index", path = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            //Start Client
            routes.MapRoute(
                name: "ProductDetail",
                url: "san-pham/{id}",
                defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ArticleCate",
                url: "tin-tuc",
                defaults: new { controller = "Article", action = "ArticleByCate" }
            );
            routes.MapRoute(
                name: "ContactPage",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index"}
            );
            routes.MapRoute(
                name: "ClientPage",
                url: "{path}",
                defaults: new { controller = "Client", action = "Index", path = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ClientPageWithid",
                url: "{path}/{id}",
                defaults: new { controller = "Client", action = "Index", path = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LoadPageInFolder",
                url: "{controller}/{action}/{folder}/{view}/",
                defaults: new { controller = "Client", action = "Index", folder = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LoadPageInSubfolder",
                url: "{controller}/{action}/{folder}/{subfolder}/{view}/",
                defaults: new { controller = "Client", action = "Index", folder = UrlParameter.Optional, subfolder = UrlParameter.Optional, id = UrlParameter.Optional }
            );
        }
    }
}