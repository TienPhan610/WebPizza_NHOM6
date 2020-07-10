using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodShopOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
            );
            routes.MapRoute(
               name: "Register",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
           );
            routes.MapRoute(
          name: "Login",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
        );
            routes.MapRoute(
           name: "AddItem",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
        );
             routes.MapRoute(
             name: "Cart",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
          );
            routes.MapRoute(
           name: "Payment",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
        );
          routes.MapRoute(
          name: "Payment Success",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
        );
          routes.MapRoute(
          name: "Payment Fail",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Cart", action = "OrderFail", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
        );
            routes.MapRoute(
          name: "Not LoggedIn",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Cart", action = "NotLoggedIn", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
        );
        }
    }
}
