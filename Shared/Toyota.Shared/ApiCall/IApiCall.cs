using Toyota.Shared.Entities.Enum;

namespace Toyota.Shared.ApiCall
{
    public interface IApiCall
    {
        public Task<TResponse> CallApi<TRequest, TResponse>(string route, TRequest requestModel, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST, bool isLog = true);
        public Task<string> CallApi<TRequest>(string route, TRequest requestModel, string token, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST,bool isLog = true);
        public Task<TResponse> CallApi<TRequest, TResponse>(string route, TRequest requestModel, string token, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST, bool isLog = true);
        public Task<TResponse> CallApi<TRequest, TResponse>(string route, TRequest requestModel, string token, string clientName, List<KeyValuePair<string, string>>? requestHeaders = null, int requestType = (int)Enums.ApiCallRequestType.POST, bool isLog = true);
        public Task<TResponse> CallApiFile<TRequest, TResponse>(string route, TRequest requestModel, string token, string clientName, StreamContent content, List<KeyValuePair<string, string>>? requestHeaders = null, int requestType = (int)Enums.ApiCallRequestType.POST, bool isLog = true);
    }
}
