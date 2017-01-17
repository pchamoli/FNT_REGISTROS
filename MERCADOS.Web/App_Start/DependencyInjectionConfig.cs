﻿using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Autofac.Integration.Wcf;
using MERCADOS.IServiciosAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace MERCADOS.Web
{
    public class DependencyInjectionConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            string baseUriString = System.Configuration.ConfigurationManager.AppSettings["baseUriService"].ToString();

            var baseUri = new Uri(baseUriString);
            builder.RegisterServiceProxy<IFormatoService>(baseUri, "FormatoService.svc", "FileStreamConfig");
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

        }
    }

    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<TChannel, SimpleActivatorData, SingleRegistrationStyle>
            RegisterServiceProxy<TChannel>(this ContainerBuilder builder, Uri
                                           baseUri, string relativeUri, string configurationName)
        {
            builder.Register(c => new ChannelFactory<TChannel>(
            string.IsNullOrEmpty(configurationName) ? new BasicHttpBinding() : new
            BasicHttpBinding(configurationName),
            new EndpointAddress(new Uri(baseUri, relativeUri)))
            ).SingleInstance();
            return builder.Register(c => c.Resolve<ChannelFactory<TChannel>>().CreateChannel())
            .UseWcfSafeRelease()
            .InstancePerRequest();  //InstancePerHttpRequest
        }
    }
}