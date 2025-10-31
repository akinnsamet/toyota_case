using Toyota.Entities.Auth;
using Toyota.Entities.Locations;
using Toyota.Entities.VehicleService;
using Toyota.Shared.ApiCall;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Entities.Enum;
using Toyota.Shared.Utilities;

namespace Toyota.Web.Services
{

    public class ApiService(IApiCall apiCall) : IApiService
    {

        public async Task<ServiceResponse<LoginResponseEntity>?> LoginAsync(LoginRequestEntity request)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response =  apiCall.CallApi<object, ServiceResponse<LoginResponseEntity>>("api/Auth/Login", request, null, Constants.HtppClientToyotaApiNoTokenServiceName, null, (int)Enums.ApiCallRequestType.POST).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
        }
      

        public async Task<ServiceResponse<List<VehicleServiceRecordEntity>>?> GetVehicleServiceRecordsAsync(VehicleServiceRecordSearchEntity request)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response = apiCall.CallApi<object, ServiceResponse<List<VehicleServiceRecordEntity>>>("api/VehicleServiceRecord/GetAll", request, null, Constants.HtppClientToyotaApiServiceName, null, (int)Enums.ApiCallRequestType.POST).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });

        }

        public async Task<ServiceResponse<VehicleServiceRecordEntity>?> GetVehicleServiceRecordAsync(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response = apiCall.CallApi<object, ServiceResponse<VehicleServiceRecordEntity>>($"api/VehicleServiceRecord?id={id}", null, null, Constants.HtppClientToyotaApiServiceName, null, (int)Enums.ApiCallRequestType.GET).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
        }

        public async Task<ServiceResponse<VehicleServiceRecordEntity>?> CreateVehicleServiceRecordAsync(VehicleServiceRecordCreateEntity request)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response = apiCall.CallApi<object, ServiceResponse<VehicleServiceRecordEntity>>("api/VehicleServiceRecord", request, null, Constants.HtppClientToyotaApiServiceName, null, (int)Enums.ApiCallRequestType.POST).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
        }

        public async Task<ServiceResponse<VehicleServiceRecordEntity>?> UpdateVehicleServiceRecordAsync(VehicleServiceRecordUpdateEntity request)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response = apiCall.CallApi<object, ServiceResponse<VehicleServiceRecordEntity>>("api/VehicleServiceRecord", request, null, Constants.HtppClientToyotaApiServiceName, null, (int)Enums.ApiCallRequestType.PUT).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
        }

        public async Task<ServiceResponse?> DeleteVehicleServiceRecordAsync(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response = apiCall.CallApi<object, ServiceResponse>($"api/VehicleServiceRecord?id={id}", null, null, Constants.HtppClientToyotaApiServiceName, null, (int)Enums.ApiCallRequestType.DELETE).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
        }

        public async Task<ServiceResponse<List<VehicleServiceRecordLogEntity>>?> GetVehicleServiceRecordLogsAsync(SearchEntity request)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response = apiCall.CallApi<object, ServiceResponse<List<VehicleServiceRecordLogEntity>>>("api/VehicleServiceRecord/GetLogAll", request, null, Constants.HtppClientToyotaApiServiceName, null, (int)Enums.ApiCallRequestType.POST).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });

        }

        public async Task<ServiceResponse<List<CityEntity>>?> GetCitiesAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response = apiCall.CallApi<object, ServiceResponse<List<CityEntity>>>("api/City", null, null, Constants.HtppClientToyotaApiServiceName, null, (int)Enums.ApiCallRequestType.GET).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
        }

        public async Task<ServiceResponse<List<string>>?> GetApplicationLogs(SearchEntity request)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var response = apiCall.CallApi<object, ServiceResponse<List<string>>>("api/ApplicationLog/GetAll", request, null, Constants.HtppClientToyotaApiServiceName, null, (int)Enums.ApiCallRequestType.POST).Result;
                    if (response == null)
                    {
                        return null;
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });

        }
    }
}
