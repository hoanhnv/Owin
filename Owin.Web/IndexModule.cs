using Nancy;
using Nancy.ModelBinding;
using Owin.Web.Models;
using Owin.Web.Models.Api;

namespace Owin.Web
{
    public class IndexModule:NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => View[new IndexViewModel {Title = "Nancy Fx + RazorViewEngine"}];
            Post["/"] = _ =>
                        {
                            var dto = this.Bind<Person>();
                            return HttpStatusCode.OK;
                        };
            Get["/Person"] = _ => new Person {Id = 1, Name = "Nemo Nobody"};
        }
    }
}