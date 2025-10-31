using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Toyota.Application.Interfaces.Data;
using Toyota.Data.Context.Toyota;

namespace Toyota.Data.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IToyotaRepository<>),typeof(ToyotaRepository<>));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.MaxDepth = 64;
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            return services;
        }
    }
}
