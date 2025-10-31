using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Toyota.Application.Api;
using Toyota.Shared.Utilities;

namespace Toyota.Application.Services
{
    public static class HttpClientServicesConfiguration
    {
        public static IServiceCollection AddHttpClientServiceCollection(this IServiceCollection services)
        {
            services.AddHttpClient(Constants.HtppClientToyotaApiServiceName, x =>
            {
                var httpContextAccessor = new HttpContextAccessor().HttpContext;
                string servicesApiUrl = ApiConfiguration.Configuration?.GetSection("Services:ToyotaApi:ApiUrl").Value ?? "";
                string apiClientToken = httpContextAccessor?.Session.GetString("AccessToken") ?? "";
                x.BaseAddress = new Uri(servicesApiUrl);
                x.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiClientToken}");
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            services.AddHttpClient(Constants.HtppClientToyotaApiNoTokenServiceName, x =>
            {
                string servicesApiUrl = ApiConfiguration.Configuration?.GetSection("Services:ToyotaApi:ApiUrl").Value ?? "";
                x.BaseAddress = new Uri(servicesApiUrl);
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            
            return services;
        }
    }
}
