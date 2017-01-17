using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;

namespace MERCADOS.Host.Modules
{
    public class AplicacionServiceModule : Autofac.Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("MERCADOS.ServicioAplicacion"))
            .Where(type => type.Name.EndsWith("Service", StringComparison.Ordinal))
            .AsImplementedInterfaces();
        }
    }
}