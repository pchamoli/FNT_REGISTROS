using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MERCADOS.IRepositorio;
using MERCADOS.Repositorio;
using MERCADOS.Repositorio.Base;
using MERCADOS.Entidades;

namespace MERCADOS.Host.Modules
{
    public class RepositorioModule : Autofac.Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("MERCADOS.Repositorio"))
            .Where(type => type.Name.EndsWith("Repositorio", StringComparison.Ordinal))
            .AsImplementedInterfaces();
            var method = typeof(RepositorioModule).GetMethod("RegisterRepository");
            var types = Assembly.Load("MERCADOS.Entidades").GetTypes();
            foreach (var type in types) method.MakeGenericMethod(type).Invoke(null, new[] { builder });
            string nameOrConnectionString = "name=DB_REGISTROSEntities";
            builder.RegisterType<DB_REGISTROSEntities>().As<IContext>().WithParameter("nameOrConnectionString",
            nameOrConnectionString).InstancePerLifetimeScope();
            builder.RegisterType<ContextSemci>().As<IContext>();
            builder.RegisterType<ContextSemci>().As<IUnitOfWork>();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void RegisterRepository<T>(ContainerBuilder builder) where T : class
        {
            builder.RegisterType<BaseRepositorio<T>>().AsImplementedInterfaces();
        }
    }
}