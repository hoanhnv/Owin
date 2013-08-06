using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Owin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureApi(app);
            ConfigureMvc(app);
            app.UseHandlerAsync((req, res) =>
            {
                res.ContentType = "text/plain";
                return res.WriteAsync("OWIN Web Application Final step");
            });
        }
    }
}