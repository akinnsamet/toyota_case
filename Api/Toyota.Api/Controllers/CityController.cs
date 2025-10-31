using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyota.Api.Authorization;
using Toyota.Application.Interfaces.Business;
using Toyota.Entities.Locations;
using Toyota.Shared.Entities.Common;

namespace Toyota.Api.Controllers.Area
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(ICityBusiness bussines) : ControllerBase
    {
        #region Methods

        [HttpGet]
        [Authorize]
        [PolicyBasedAuthorize]
        [Route("")]
        [ProducesResponseType(typeof(ServiceResponse<List<CityEntity>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await bussines.GetAll();
            return Ok(result);
        }

        #endregion
    }
}
