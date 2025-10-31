using Toyota.Application.Api;
using Toyota.Application.Domain;
using Toyota.Entities.Locations;
using Toyota.Entities.User;
using Toyota.Entities.VehicleService;
using Toyota.Shared.Entities.User;
using Toyota.Shared.Mapping;

namespace Toyota.Application.Mapping
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return MappingConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        #region Login

        public static LoginResponseUserEntity ToLoginResponseUserEntityModel(this User data)
        {
            return data.MapTo<User, LoginResponseUserEntity>();

        }
        public static CurrentUserEntity ToCurrentUserModel(this User data)
        {
            return data.MapTo<User, CurrentUserEntity>();
        }

        #endregion Login

        #region User

        public static UserEntity ToUserEntityModel(this User data)
        {
            return data.MapTo<User, UserEntity>();
        }

        #endregion User


        #region VehicleService

        public static VehicleServiceRecordEntity ToModel(this VehicleServiceRecord data)
        {
            return data.MapTo<VehicleServiceRecord, VehicleServiceRecordEntity>();
        }

        public static VehicleServiceRecord FromCreateModel(this VehicleServiceRecordCreateEntity data)
        {
            var record = data.MapTo<VehicleServiceRecordCreateEntity, VehicleServiceRecord>();
            record.CreateUserId = ApiConfiguration.CurrentUser.Id ?? 0;

            return record;
        }

        public static VehicleServiceRecord FromUpdateModel(this VehicleServiceRecordUpdateEntity data, VehicleServiceRecord existEntity)
        {
            var record = data.MapTo<VehicleServiceRecordUpdateEntity, VehicleServiceRecord>();

            record.CreateUserId = existEntity.CreateUserId;
            record.CreateDate = existEntity.CreateDate;
            record.LastUpdateDate = DateTime.UtcNow;
            record.UpdateUserId = ApiConfiguration.CurrentUser.Id ?? 0;

            return record;
        }

        public static VehicleServiceRecordLogEntity ToModel(this VehicleServiceRecordLog data)
        {
            return data.MapTo<VehicleServiceRecordLog, VehicleServiceRecordLogEntity>();
        }

        #endregion VehicleService

        #region Locations

        public static CityEntity ToModel(this City data)
        {
            return data.MapTo<City, CityEntity>();
        }

        #endregion Locations


    }
}
