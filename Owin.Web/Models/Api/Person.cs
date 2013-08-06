using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Owin.Web.Models.Api
{
    [DataContract]
    public class Person
    {
        [DataMember(Order= 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }
    }
}