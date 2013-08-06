using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiContrib.Formatting;

namespace Owin.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Add(new ProtoBufFormatter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
