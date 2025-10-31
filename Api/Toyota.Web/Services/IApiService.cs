using Toyota.Entities.Auth;
using Toyota.Entities.Locations;
using Toyota.Entities.VehicleService;
using Toyota.Shared.Entities.Common;

namespace Toyota.Web.Services
{
    public interface IApiService
    {
        Task<ServiceResponse<LoginResponseEntity>?> LoginAsync(LoginRequestEntity request);
        Task<ServiceResponse<List<VehicleServiceRecordEntity>>?> GetVehicleServiceRecordsAsync(VehicleServiceRecordSearchEntity request);
        Task<ServiceResponse<VehicleServiceRecordEntity>?> GetVehicleServiceRecordAsync(int id);
        Task<ServiceResponse<VehicleServiceRecordEntity>?> CreateVehicleServiceRecordAsync(VehicleServiceRecordCreateEntity request);
        Task<ServiceResponse<VehicleServiceRecordEntity>?> UpdateVehicleServiceRecordAsync(VehicleServiceRecordUpdateEntity request);
        Task<ServiceResponse?> DeleteVehicleServiceRecordAsync(int id);
        Task<ServiceResponse<List<VehicleServiceRecordLogEntity>>?> GetVehicleServiceRecordLogsAsync(SearchEntity request);
        Task<ServiceResponse<List<CityEntity>>?> GetCitiesAsync();
        Task<ServiceResponse<List<string>>?> GetApplicationLogs(SearchEntity request);
    }

}
