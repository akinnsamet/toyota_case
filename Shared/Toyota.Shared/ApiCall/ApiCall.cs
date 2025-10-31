using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Toyota.Shared.Entities.Enum;
using Toyota.Shared.Extensions;

namespace Toyota.Shared.ApiCall
{
    public class ApiCall : IApiCall
    {
        #region Fileds

        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ApiCall> _logger;

        #endregion

        #region Ctor
        public ApiCall(IHttpClientFactory clientFactory, ILogger<ApiCall> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        #endregion

        #region Methods

        public async Task<string> CallApi<TRequest>(string route, TRequest requestModel, string token, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST, bool isLog = false)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(requestModel);
              
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = _clientFactory.CreateClient(clientName))
                {
                    var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    client.Timeout = TimeSpan.FromMinutes(15);
                    HttpResponseMessage response = new HttpResponseMessage();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    switch (requestType)
                    {
                        case (int)Enums.ApiCallRequestType.GET:
                            response = await client.GetAsync($"{client.BaseAddress}{route}");
                            break;
                        case (int)Enums.ApiCallRequestType.POST:
                            response = await client.PostAsync($"{client.BaseAddress}{route}", stringContent);
                            break;
                        case (int)Enums.ApiCallRequestType.PUT:
                            response = await client.PutAsync($"{client.BaseAddress}{route}", stringContent);
                            break;
                        case (int)Enums.ApiCallRequestType.DELETE:
                            response = await client.DeleteAsync($"{client.BaseAddress}{route}");
                            break;
                    }

                    var responseModel = await response.Content.ReadAsStringAsync();
                   
                    return responseModel;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ApiCall Excepiton :{ErrorHelper.GetExceptionString(ex)}");
                return null;
            }
        }
        public async Task<TResponse> CallApi<TRequest, TResponse>(string route, TRequest requestModel, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST,bool isLog = false)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(requestModel);
              
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = _clientFactory.CreateClient(clientName))
                {

                    var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    client.Timeout = TimeSpan.FromMinutes(15);
                    HttpResponseMessage response = new HttpResponseMessage();
                    switch (requestType)
                    {
                        case (int)Enums.ApiCallRequestType.GET:
                            if (requestModel == null)
                                response = await client.GetAsync($"{client.BaseAddress}{route}");
                            else
                                response = await client.GetAsync($"{client.BaseAddress}{route}/{requestModel}");
                            break;
                        case (int)Enums.ApiCallRequestType.POST:
                            response = await client.PostAsync($"{client.BaseAddress}{route}", stringContent);
                            break;
                        case (int)Enums.ApiCallRequestType.PUT:
                            response = await client.PutAsync($"{client.BaseAddress}{route}", stringContent);
                            break;
                        case (int)Enums.ApiCallRequestType.DELETE:
                            response = await client.DeleteAsync($"{client.BaseAddress}{route}");
                            break;
                    }

                    var responseModel = await response.Content.ReadAsStreamAsync();
                   
                    if (responseModel.Length <= 0)
                    {
                        _logger.LogError($"ApiCall Response Empty : Route", $"{client.BaseAddress}{route}");
                        return default(TResponse);
                    }
                    var result = await JsonSerializer.DeserializeAsync<TResponse>(responseModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ApiCall Excepiton :{ErrorHelper.GetExceptionString(ex)}");
                return default(TResponse);
            }
        }
        public async Task<TResponse> CallApi<TRequest, TResponse>(string route, TRequest requestModel, string token, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST,bool isLog = false)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(requestModel);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = _clientFactory.CreateClient(clientName))
                {

                    var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    client.Timeout = TimeSpan.FromMinutes(15);
                    HttpResponseMessage response = new HttpResponseMessage();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    switch (requestType)
                    {
                        case (int)Enums.ApiCallRequestType.GET:
                            if (requestModel == null)
                                response = await client.GetAsync($"{client.BaseAddress}{route}");
                            else
                                response = await client.GetAsync($"{client.BaseAddress}{route}/{requestModel}");
                            break;
                        case (int)Enums.ApiCallRequestType.POST:
                            response = await client.PostAsync($"{client.BaseAddress}{route}", stringContent);
                            break;
                        case (int)Enums.ApiCallRequestType.PUT:
                            response = await client.PutAsync($"{client.BaseAddress}{route}", stringContent);
                            break;
                        case (int)Enums.ApiCallRequestType.DELETE:
                            response = await client.DeleteAsync($"{client.BaseAddress}{route}");
                            break;
                    }

                    var responseModel = await response.Content.ReadAsStreamAsync();

                    var result = await JsonSerializer.DeserializeAsync<TResponse>(responseModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ApiCall Excepiton :{ErrorHelper.GetExceptionString(ex)}");
                return default(TResponse);
            }
        }
        public async Task<TResponse> CallApi<TRequest, TResponse>(string route, TRequest requestModel, string token, string clientName, List<KeyValuePair<string, string>>? requestHeaders = null, int requestType = (int)Enums.ApiCallRequestType.POST, bool isLog = false)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(requestModel);

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = _clientFactory.CreateClient(clientName))
                {
                    HttpContent stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

                    client.Timeout = TimeSpan.FromMinutes(15);
                    HttpResponseMessage response = new HttpResponseMessage();
                    if (requestHeaders != null)
                    {
                        foreach (var requestHeader in requestHeaders)
                        {
                            client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(token))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    }


                    switch (requestType)
                    {
                        case (int)Enums.ApiCallRequestType.GET:
                            response = await client.GetAsync($"{client.BaseAddress}{route}");
                            break;
                        case (int)Enums.ApiCallRequestType.POST:
                            response = await client.PostAsync($"{client.BaseAddress}{route}", stringContent);
                            break;
                        case (int)Enums.ApiCallRequestType.PUT:
                            response = await client.PutAsync($"{client.BaseAddress}{route}", stringContent);
                            break;
                        case (int)Enums.ApiCallRequestType.DELETE:
                            response = await client.DeleteAsync($"{client.BaseAddress}{route}");
                            break;
                    }

                    var responseModel = await response.Content.ReadAsStreamAsync();

                    var result = await JsonSerializer.DeserializeAsync<TResponse>(responseModel);

                    var responseJson = JsonSerializer.Serialize(result);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ApiCall Excepiton :{ErrorHelper.GetExceptionString(ex)}");
                return default(TResponse);
            }
        }
        public async Task<TResponse> CallApiFile<TRequest, TResponse>(string route, TRequest requestModel, string token, string clientName, StreamContent content, List<KeyValuePair<string, string>>? requestHeaders = null, int requestType = (int)Enums.ApiCallRequestType.POST, bool isLog = false)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(requestModel);

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = _clientFactory.CreateClient(clientName))
                {
                    client.Timeout = TimeSpan.FromMinutes(15);
                    HttpResponseMessage response = new HttpResponseMessage();
                    if (requestHeaders != null)
                    {
                        foreach (var requestHeader in requestHeaders)
                        {
                            client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
                        }
                    }
                    else
                    {
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    }

                    
                    switch (requestType)
                    {
                        case (int)Enums.ApiCallRequestType.GET:
                            response = await client.GetAsync($"{client.BaseAddress}{route}");
                            break;
                        case (int)Enums.ApiCallRequestType.POST:
                            response = await client.PostAsync($"{client.BaseAddress}{route}", content);
                            break;
                        case (int)Enums.ApiCallRequestType.PUT:
                            response = await client.PutAsync($"{client.BaseAddress}{route}", content);
                            break;
                        case (int)Enums.ApiCallRequestType.DELETE:
                            response = await client.DeleteAsync($"{client.BaseAddress}{route}");
                            break;
                    }

                    var responseModel = JsonSerializer.Serialize(response.Headers);

                    var result = JsonSerializer.Deserialize<TResponse>(responseModel);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ApiCall Excepiton :{ErrorHelper.GetExceptionString(ex)}");
                return default(TResponse);
            }
        }
        #endregion

    }
}
