using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Owin.Web.Formatters;

namespace Owin.Web.App_Start
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        //protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        //{
            
        //}

        //protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        //{
        //    //This can be achieved by not calling the "ConfigureApplicationContainer" base,
        //    //thus not configuring it to use AutoRegister.
        //    //base.ConfigureApplicationContainer (container);
        //}

        //protected override NancyInternalConfiguration InternalConfiguration
        //{
        //    get
        //    {
        //        return NancyInternalConfiguration.WithOverrides(c => c.Serializers.Insert(0, typeof(ServiceStackJsonFormatter)));
        //    }
        //}
    }
}