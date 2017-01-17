using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;
using MERCADOS.Host.Modules;

namespace MERCADOS.Host
{
    public static class DependencyInjectionHelper
    {
        public static void LoadContainer()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule<ServiciosDominioModule>();
            builder.RegisterModule<RepositorioModule>();
            builder.RegisterModule<AplicacionServiceModule>();
            builder.RegisterInstance(CultureInfo.CurrentCulture).As<IFormatProvider>();

            AutofacHostFactory.Container = builder.Build();
        }
    }
}