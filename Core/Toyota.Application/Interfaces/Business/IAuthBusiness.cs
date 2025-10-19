using Toyota.Entities.Auth;
using Toyota.Entities.User;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Entities.Common.Interface;

namespace Toyota.Application.Interfaces.Business
{
    public interface IAuthBusiness:IBaseBusiness
    {
        public Task<ServiceResponse<LoginResponseUserEntity>> Login(LoginRequestEntity request);
    }
}
