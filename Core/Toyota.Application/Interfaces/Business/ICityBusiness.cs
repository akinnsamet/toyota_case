using Toyota.Entities.Locations;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Entities.Common.Interface;

namespace Toyota.Application.Interfaces.Business
{
    public interface ICityBusiness : IBaseBusiness
    {
        public Task<ServiceResponse<List<CityEntity>>> GetAll();
    }
}

