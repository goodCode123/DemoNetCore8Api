using Autofac;
using Infrastructure.Attributes;
using Repository.Dal;
using Repository.InterFace;
using Services.InterFace;
using Services.Service;
using System.Reflection;

namespace DemoNetCore8Api.Service.Ioc
{
    public class SwaggerModule : Autofac.Module
    {
        /// <summary>
        /// 載入DI Container
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            Assembly[] domainAssembly = new Assembly[] { Assembly.Load("Repository") };

            LifetimeRegistry(builder, domainAssembly);

            Assembly[] serviceAssembly = new Assembly[] { Assembly.Load("Services") };
            LifetimeRegistry(builder, serviceAssembly);
        }

        /// <summary>
        /// 生命週期註冊
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        private void LifetimeRegistry(ContainerBuilder builder, Assembly[] assembly)
        {
            builder.RegisterGeneric(typeof(CurdService<>)).As(typeof(ICrudService<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(GenericCrudDal<>)).As(typeof(ICrudDal<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetCustomAttribute<PerLifetimeScopeServiceAttribute>() != null)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetCustomAttribute<SingleServiceAttribute>() != null)
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired();
        }
    }
}
