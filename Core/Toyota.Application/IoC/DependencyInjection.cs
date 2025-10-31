using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Toyota.Application.Services;
using Toyota.Application.Validators;
using Toyota.Shared.ApiCall;
using Toyota.Shared.Helpers.Swagger;
using Toyota.Shared.Mapping;
using Swashbuckle.AspNetCore.Filters;

namespace Toyota.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IApiCall, ApiCall>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Toyota Swagger", Version = "v1" });


                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.OperationFilter<AddRequiredHeaderParameter>();

                c.ExampleFilters();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            });

            services.AddValidatorConfigurationLayer();

            services.AddHttpClientServiceCollection();

            MappingConfiguration.Init();

            return services;
        }
        public static IServiceCollection AddMessagingApplicationLayer(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IApiCall, ApiCall>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Toyota Messaging Swagger", Version = "v1" });


                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                                                                             
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.OperationFilter<AddRequiredHeaderParameter>();

                c.ExampleFilters();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            });


            services.AddValidatorConfigurationLayer();

            services.AddHttpClientServiceCollection();

            MappingConfiguration.Init();

            return services;
        }
        public static IServiceCollection AddHangfireApplicationLayer(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddSingleton<IApiCall, ApiCall>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hangfire Swagger", Version = "v1" });


                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            });

            services.AddHttpClientServiceCollection();

            MappingConfiguration.Init();

            return services;
        }
        public static IServiceCollection AddMcWebApplicationLayer(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddSingleton<IApiCall, ApiCall>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SPanel Swagger", Version = "v1" });


                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            });

            services.AddHttpClientServiceCollection();

            MappingConfiguration.Init();

            return services;
        }
    }
}
