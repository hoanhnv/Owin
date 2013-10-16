using System.Collections.Generic;
using Nancy.Security;

namespace Owin.Web.Authentication.Forms
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}