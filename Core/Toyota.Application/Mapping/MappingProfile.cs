using AutoMapper;
using Toyota.Application.Domain;
using Toyota.Entities.Locations;
using Toyota.Entities.User;
using Toyota.Entities.VehicleService;
using Toyota.Shared.Entities.User;
using Toyota.Shared.Mapping;

namespace Toyota.Application.Mapping
{
    public class MappingProfile : Profile, IMapFrom
    {
        public MappingProfile()
        {
            #region Login

            CreateMap<User, LoginResponseUserEntity>();
            CreateMap<User, CurrentUserEntity>();

            #endregion

            #region User

            CreateMap<User, UserEntity>();

            #endregion User

            #region VehicleService

            CreateMap<VehicleServiceRecord, VehicleServiceRecordEntity>();

            CreateMap<VehicleServiceRecordCreateEntity, VehicleServiceRecord>();

            CreateMap<VehicleServiceRecordUpdateEntity, VehicleServiceRecord>();

            CreateMap<VehicleServiceRecordLog, VehicleServiceRecordLogEntity>();

            #endregion VehicleService

            #region Locations

            CreateMap<City, CityEntity>();

            #endregion Locations

        }

        public int Order => 0;
    }
}
