using System.Web.Http;
using Owin.Web.Formatters;

namespace Owin.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var index = config.Formatters.IndexOf(config.Formatters.JsonFormatter);
            config.Formatters.RemoveAt(index);
            config.Formatters.Insert(index, new ServiceStackJsonFormatter()); // //Content-Type: application/json
            config.Formatters.Add(new ProtoBufFormatter()); //Content-Type: application/x-protobuf

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
