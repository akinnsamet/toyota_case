using Microsoft.EntityFrameworkCore;
using System.Data;
using Toyota.Application.Api;
using Toyota.Application.Interfaces.Business;
using Toyota.Application.Interfaces.Data;
using Toyota.Application.Mapping;
using Toyota.Business.Common;
using Toyota.Data.Context.Toyota;
using Toyota.Entities.VehicleService;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Extensions;
using Toyota.Shared.Helpers;
using Toyota.Shared.Utilities;

namespace Toyota.Business.Country
{
    public class VehicleServiceBusiness : ToyotaBaseBusiness, IVehicleServiceBusiness
    {
        #region Fields

        private readonly IToyotaRepository<Application.Domain.VehicleServiceRecord> _vehicleServiceRecordRepository;
        private readonly IToyotaRepository<Application.Domain.VehicleServiceRecordLog> _vehicleServiceRecordLogRepository;
        private readonly IToyotaDbContext _context;

        #endregion Fields

        #region Ctor
        public VehicleServiceBusiness(
            IToyotaRepository<Application.Domain.VehicleServiceRecord> vehicleServiceRecordRepository,
            IToyotaRepository<Application.Domain.VehicleServiceRecordLog> vehicleServiceRecordLogRepository,
            IToyotaDbContext context
            ) : base()
        {
            _vehicleServiceRecordRepository = vehicleServiceRecordRepository;
            _vehicleServiceRecordLogRepository = vehicleServiceRecordLogRepository;
            _context = context;
        }

        #endregion Ctor

        #region Methods
        public async Task<ServiceResponse<List<VehicleServiceRecordEntity>>> GetAll(VehicleServiceRecordSearchEntity request)
        {
            await using var tx = await _context.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var query = _vehicleServiceRecordRepository.GetListNoTracking().Where(x => x.DeletedDate == null);
                if (request.SearchText.IsNotNullOrNotWhiteSpace())
                {
                    var searchText = request.SearchText!.Trim().ToLower();
                    query = query.Where(u =>
                     (u.LicensePlate != null && u.LicensePlate.Trim().ToLower().Contains(searchText)) ||
                     (u.BrandName != null && u.BrandName.Trim().ToLower().Contains(searchText)) ||
                     (u.ModelName != null && u.ModelName.Trim().ToLower().Contains(searchText)) ||
                     (u.ServiceCityId != null && u.ServiceCity.CityName.ToLower().Contains(searchText)) ||
                     (u.ServiceNote != null && u.ServiceNote.Trim().ToLower().Contains(searchText)));
                }


                var count = await query.CountAsync();
                var orderQueryable = query.Include(x => x.ServiceCity).OrderBy(x => x.Id);
                query = orderQueryable.ApplySortingPaging(request.SortingPaging!);
                var responseModel = await query.Select(x => x.ToModel()).ToListAsync();

                return new ServiceResponse<List<VehicleServiceRecordEntity>>(responseModel, count);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<VehicleServiceRecordEntity>>(Constants.ErrorCode, Constants.ExceptionErrorMessage);
            }
        }

        public async Task<ServiceResponse<VehicleServiceRecordEntity>> Find(int id)
        {
            await using var tx = await _context.BeginTransaction(IsolationLevel.ReadUncommitted);

            var result = await _vehicleServiceRecordRepository.GetListNoTracking().Include(x => x.ServiceCity).FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null);
            if (result.IsNull())
            {
                return new ServiceResponse<VehicleServiceRecordEntity>(Constants.ErrorCode, Constants.EmptyErrorMessage);
            }

            return new ServiceResponse<VehicleServiceRecordEntity>(result!.ToModel());
        }

        public async Task<ServiceResponse<VehicleServiceRecordEntity>> Create(VehicleServiceRecordCreateEntity request)
        {
            await using var tx = await _context.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                DateTime? now = DateTime.Now;
                //if (request.AlreayExists == false)
                //{
                //    var recordExistsToday = await _vehicleServiceRecordRepository.GetListNoTracking().AnyAsync(x => x.LicensePlate == request.LicensePlate && x.CreateDate >= now.DayFirstMoment() && x.CreateDate <= now.DayLastMoment());
                //    if (recordExistsToday)
                //    {
                //        return new ServiceResponse<VehicleServiceRecordEntity>(Constants.ErrorCode, "Bu Plaka için bugün kayıt oluşturulmuş yine de kaydetmek ister misiniz?");
                //    }
                //}

                var insertEntity = request.FromCreateModel();

                await _vehicleServiceRecordRepository.Insert(insertEntity);
                await _vehicleServiceRecordLogRepository.Insert(new Application.Domain.VehicleServiceRecordLog()
                {
                    Description = $"{request.LicensePlate} plakalı araç için servis girişi yapıldı, UserId : {ApiConfiguration.CurrentUser.Id} , Name : {ApiConfiguration.CurrentUser.FullName}"
                });

                await tx.CommitAsync();
                return new ServiceResponse<VehicleServiceRecordEntity>(insertEntity.ToModel());
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new ServiceResponse<VehicleServiceRecordEntity>(Constants.ErrorCode, Constants.ExceptionErrorMessage);
            }
        }

        public async Task<ServiceResponse<VehicleServiceRecordEntity>> Update(VehicleServiceRecordUpdateEntity request)
        {
            await using var tx = await _context.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var existingEntity = await _vehicleServiceRecordRepository.GetListNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
                if (existingEntity == null)
                {
                    return new ServiceResponse<VehicleServiceRecordEntity>(Constants.ErrorCode, Constants.EmptyErrorMessage);
                }

                var updateEntity = request.FromUpdateModel(existingEntity);
                var changeLog = LogHelper.GenerateChangeLog(existingEntity, updateEntity, $"{request.LicensePlate} için servis kaydı güncelleme, ancak değişiklik yok.", $"{request.LicensePlate} plakalı araç =>  UserId : {ApiConfiguration.CurrentUser.Id} , Name : {ApiConfiguration.CurrentUser.FullName}");

                await _vehicleServiceRecordRepository.Update(updateEntity);

                await _vehicleServiceRecordLogRepository.Insert(new Application.Domain.VehicleServiceRecordLog()
                {
                    Description = changeLog
                });
                await tx.CommitAsync();

                return new ServiceResponse<VehicleServiceRecordEntity>(updateEntity.ToModel());
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new ServiceResponse<VehicleServiceRecordEntity>(Constants.ErrorCode, Constants.ExceptionErrorMessage);
            }
        }

        public async Task<ServiceResponse> Delete(long id)
        {
            await using var tx = await _context.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var existingEntity = await _vehicleServiceRecordRepository.GetList().FirstOrDefaultAsync(x => x.Id == id);
                if (existingEntity.IsNull())
                {
                    return new ServiceResponse(Constants.ErrorCode, Constants.EmptyErrorMessage);
                }

                existingEntity!.DeleteUserId = ApiConfiguration.CurrentUser.Id;
                existingEntity.DeletedDate = DateTime.UtcNow;

                await _vehicleServiceRecordLogRepository.Insert(new Application.Domain.VehicleServiceRecordLog()
                {
                    Description = $"{existingEntity!.LicensePlate} plakalı araç kayıtlardan silindi, UserId : {ApiConfiguration.CurrentUser.Id} , Name : {ApiConfiguration.CurrentUser.FullName}"
                });

                await _vehicleServiceRecordRepository.Update(existingEntity!);

                await tx.CommitAsync();
                return new ServiceResponse();
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new ServiceResponse(Constants.ErrorCode, Constants.ExceptionErrorMessage);
            }
        }

        public async Task<ServiceResponse<List<VehicleServiceRecordLogEntity>>> GetLogAll(SearchEntity request)
        {
            await using var tx = await _context.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var query = _vehicleServiceRecordLogRepository.GetListNoTracking();
                if (request.SearchText.IsNotNullOrNotWhiteSpace())
                {
                    var searchText = request.SearchText!.Trim().ToLower();
                    query = query.Where(u => u.Description != null && u.Description.Trim().ToLower().Contains(searchText));
                }

                var count = await query.CountAsync();
                var orderQueryable = query.OrderBy(x => x.Id);
                query = orderQueryable.ApplySortingPaging(request.SortingPaging!);
                var responseModel = await query.Select(x => x.ToModel()).ToListAsync();

                return new ServiceResponse<List<VehicleServiceRecordLogEntity>>(responseModel, count);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<VehicleServiceRecordLogEntity>>(Constants.ErrorCode, Constants.ExceptionErrorMessage);
            }
        }

        #endregion Methods

    }
}
