using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Owin.Web.Models.Api;

namespace Owin.Web.Controllers.Api
{
    public class ValuesController : ApiController
    {
        public Person Get()
        {
            return new Person {Id = 1, Name = "Nemo Nobody"};
        }
    }
}