using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyota.Api.Authorization;
using Toyota.Application.Interfaces.Business;
using Toyota.Entities.Auth;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Entities.Enum;
using Toyota.Shared.Utilities;

namespace Toyota.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthBusiness authBusiness) : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        [ProducesResponseType(typeof(ServiceResponse<LoginResponseEntity>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginRequestEntity req)
        {
            var tokenModel = await authBusiness.Login(req);

            if (tokenModel.Result.ResultCode != Constants.SuccessCode)
            {
                return Ok(new ServiceResponse<LoginResponseEntity>()
                {
                    ReturnObject = new LoginResponseEntity(),
                    Result = tokenModel.Result,
                    TotalCount = tokenModel.TotalCount
                });
            }

        
            #region LoginProcess

            var result = new LoginResponseEntity() { 
                User = tokenModel.ReturnObject
            };


            var accessTokenResult = JwtToken.GetToken(tokenModel.ReturnObject);

            result.AccessToken = accessTokenResult.Token;
            result.TokenExpiry = accessTokenResult.ExpiresDate;

            return Ok(new ServiceResponse<LoginResponseEntity>(result));

            #endregion
        }

    }
}
