using Toyota.Entities.VehicleService;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Entities.Common.Interface;

namespace Toyota.Application.Interfaces.Business
{
    public interface IVehicleServiceBusiness : IBaseBusiness
    {
        public Task<ServiceResponse<List<VehicleServiceRecordEntity>>> GetAll(VehicleServiceRecordSearchEntity request);
        public Task<ServiceResponse<VehicleServiceRecordEntity>> Find(int id);
        public Task<ServiceResponse<VehicleServiceRecordEntity>> Create(VehicleServiceRecordCreateEntity request);
        public Task<ServiceResponse<VehicleServiceRecordEntity>> Update(VehicleServiceRecordUpdateEntity request);
        public Task<ServiceResponse> Delete(long id);
        public Task<ServiceResponse<List<VehicleServiceRecordLogEntity>>> GetLogAll(SearchEntity request);
    }
}

