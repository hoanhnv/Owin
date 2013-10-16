using System;
using System.Dynamic;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using Nancy.Security;
using Owin.Web.Authentication.Forms;
using Owin.Web.Models;

namespace Owin.Web
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ =>
            {
                //this.RequiresAuthentication();
                var indexViewModel = new IndexViewModel
                {
                    UserName = "no",//Context.CurrentUser.UserName,
                    Title = "Nancy Fx + RazorViewEngine"
                };
                return View[indexViewModel];
            };

            Get["/login"] = x =>
            {
                dynamic model = new ExpandoObject();
                model.Errored = Request.Query.error.HasValue;

                return View["login", model];
            };

            Post["/login"] = x =>
            {
                var userGuid = UserMapper.ValidateUser((string)Request.Form.Username, (string)Request.Form.Password);

                if (userGuid == null)
                {
                    return Context.GetRedirect("~/login?error=true&username=" + (string)Request.Form.Username);
                }

                DateTime? expiry = null;
                if (Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };

            Get["/logout"] = x => this.LogoutAndRedirect("~/");
        }
    }
}