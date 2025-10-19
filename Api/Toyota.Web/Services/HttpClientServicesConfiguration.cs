using Toyota.Shared.Utilities;

namespace Toyota.Web.Services
{
    public static class HttpClientServicesConfiguration
    {
        public static IServiceCollection AddHttpClientServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(Constants.HtppClientToyotaApiServiceName, x =>
            {
                var httpContextAccessor = new HttpContextAccessor().HttpContext;
                string servicesApiUrl = configuration["Services:ToyotaApi:ApiUrl"] ?? "";
                string apiClientToken = httpContextAccessor?.Session.GetString("AccessToken") ?? "";
                x.BaseAddress = new Uri(servicesApiUrl);
                x.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiClientToken}");
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            services.AddHttpClient(Constants.HtppClientToyotaApiNoTokenServiceName, x =>
            {
                string servicesApiUrl = configuration["Services:ToyotaApi:ApiUrl"] ?? "";
                x.BaseAddress = new Uri(servicesApiUrl);
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            
            return services;
        }
    }
}
