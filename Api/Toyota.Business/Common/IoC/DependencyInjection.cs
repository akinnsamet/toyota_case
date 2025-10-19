using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Toyota.Shared.Entities.Common.Interface;

namespace Toyota.Business.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.GetInterfaces().Contains(typeof(IBaseBusiness)))
                .IgnoreThisInterface<IBaseBusiness>()
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
