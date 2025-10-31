using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyota.Api.Authorization;
using Toyota.Application.Interfaces.Business;
using Toyota.Entities.VehicleService;
using Toyota.Shared.Entities.Common;

namespace Toyota.Api.Controllers.Area
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleServiceRecordController(IVehicleServiceBusiness bussines) : ControllerBase
    {
        #region Methods

        [HttpPost]
        [Authorize]
        [PolicyBasedAuthorize]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ServiceResponse<List<VehicleServiceRecordEntity>>), 200)]
        public async Task<IActionResult> GetAll([FromBody] VehicleServiceRecordSearchEntity req)
        {
            var result = await bussines.GetAll(req);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [PolicyBasedAuthorize]
        [Route("")]
        [ProducesResponseType(typeof(ServiceResponse<VehicleServiceRecordEntity>), 200)]
        public async Task<IActionResult> Find([FromQuery] int id)
        {
            var result = await bussines.Find(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [PolicyBasedAuthorize]
        [Route("")]
        [ProducesResponseType(typeof(ServiceResponse<VehicleServiceRecordEntity>), 200)]
        public async Task<IActionResult> Create([FromBody] VehicleServiceRecordCreateEntity req)
        {
            var result = await bussines.Create(req);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [PolicyBasedAuthorize]
        [Route("")]
        [ProducesResponseType(typeof(ServiceResponse<VehicleServiceRecordEntity>), 200)]
        public async Task<IActionResult> Update([FromBody] VehicleServiceRecordUpdateEntity req)
        {
            var result = await bussines.Update(req);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        [PolicyBasedAuthorize]
        [Route("")]
        [ProducesResponseType(typeof(ServiceResponse), 200)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await bussines.Delete(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [PolicyBasedAuthorize]
        [Route("GetLogAll")]
        [ProducesResponseType(typeof(ServiceResponse<List<VehicleServiceRecordLogEntity>>), 200)]
        public async Task<IActionResult> GetLogAll([FromBody] SearchEntity req)
        {
            var result = await bussines.GetLogAll(req);
            return Ok(result);
        }

        #endregion
    }
}
