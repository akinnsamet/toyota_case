using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Toyota.Entities.VehicleService;
using Toyota.Web.Attributes;
using Toyota.Web.Services;

namespace Toyota.Web.Controllers
{
    [SessionAuthorize]
    public class VehicleServiceRecordLogController : Controller
    {
        private readonly IApiService _apiService;

        public VehicleServiceRecordLogController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] VehicleServiceRecordLogSearchEntity request)
        {
            var response = await _apiService.GetVehicleServiceRecordLogsAsync(request);
            return Json(new
            {
                draw = request.Draw,
                data = response?.ReturnObject,
                recordsTotal = response?.TotalCount ?? 0,
                recordsFiltered = response?.TotalCount ?? 0
            });
        }

    }
}
