using System.Web.Http;
using Owin;

namespace Owin.Web
{
    public partial class Startup
    {
        public void ConfigureApi(IAppBuilder app)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            app.UseWebApi(GlobalConfiguration.Configuration);
        }

        private void ConfigureMvc(IAppBuilder app)
        {
            
        }
    }
}