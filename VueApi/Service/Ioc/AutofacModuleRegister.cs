using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace DemoNetCore8Api.Service.Ioc
{
    public static class AutofacModuleRegister
    {
        /// <summary>
        /// 加入Autofac服務
        /// </summary>
        /// <param name="builder"></param>
        /// <summary>
        /// 加入Autofac服務
        /// </summary>
        /// <param name="builder"></param>
        public static void AddAutofac(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(autofac => autofac.RegisterModule(new SwaggerModule()));
        }
    }
}
