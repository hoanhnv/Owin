using Nancy.Owin;
using Owin.Web.App_Start;

namespace Owin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureApi(app);
            app.UseNancy(new NancyOptions { Bootstrapper = new NancyBootstrapper() });
            app.UseHandlerAsync((req, res) =>
            {
                res.ContentType = "text/plain";
                return res.WriteAsync("OWIN Web Application Final step");
            });
        }
    }
}