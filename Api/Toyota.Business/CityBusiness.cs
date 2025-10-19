using Microsoft.EntityFrameworkCore;
using System.Data;
using Toyota.Application.Interfaces.Business;
using Toyota.Application.Interfaces.Data;
using Toyota.Application.Mapping;
using Toyota.Business.Common;
using Toyota.Data.Context.Toyota;
using Toyota.Entities.Locations;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Utilities;

namespace Toyota.Business.Country
{
    public class CityBusiness : ToyotaBaseBusiness, ICityBusiness
    {
        #region Fields

        private readonly IToyotaRepository<Application.Domain.City> _cityRepository;
        private readonly IToyotaDbContext _context;

        #endregion Fields

        #region Ctor
        public CityBusiness(
            IToyotaRepository<Application.Domain.City> cityRepository,
            IToyotaDbContext context
            ) : base()
        {
            _cityRepository = cityRepository;
            _context = context;
        }

        #endregion Ctor

        #region Methods
        public async Task<ServiceResponse<List<CityEntity>>> GetAll()
        {
            await using var tx = await _context.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var query = _cityRepository.GetListNoTracking();
                var count = await query.CountAsync();
                var responseModel = await query.Select(x => x.ToModel()).ToListAsync();

                return new ServiceResponse<List<CityEntity>>(responseModel, count);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<CityEntity>>(Constants.ErrorCode, Constants.ExceptionErrorMessage);
            }
        }

        #endregion Methods

    }
}
