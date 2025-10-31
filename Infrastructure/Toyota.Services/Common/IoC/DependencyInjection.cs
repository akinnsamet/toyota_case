using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Toyota.Shared.Entities.Common.Interface;

namespace Toyota.Services.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesLayer(this IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.GetInterfaces().Contains(typeof(IBaseIntegration)))
                .IgnoreThisInterface<IBaseIntegration>()
                .AsPublicImplementedInterfaces(ServiceLifetime.Singleton);
                
            return services;
        }
    }
}
