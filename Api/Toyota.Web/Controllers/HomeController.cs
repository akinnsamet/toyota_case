using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Toyota.Entities.VehicleService;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Utilities;
using Toyota.Web.Attributes;
using Toyota.Web.Services;

namespace Toyota.Web.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        private readonly IApiService _apiService;

        public HomeController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] VehicleServiceRecordSearchEntity request)
        {
            var response = await _apiService.GetVehicleServiceRecordsAsync(request);
            return Json(new
            {
                draw = request.Draw,
                data = response?.ReturnObject,
                recordsTotal = response?.TotalCount ?? 0,
                recordsFiltered = response?.TotalCount ?? 0
            });
        }

        [HttpGet]
        [Route("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            var response = await _apiService.GetCitiesAsync();
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _apiService.GetVehicleServiceRecordAsync(id);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleServiceRecordCreateEntity request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ServiceResponse<VehicleServiceRecordEntity>(Constants.ErrorCode, "Girilen bilgileri lütfen kontrol ediniz"));
            }

            var response = await _apiService.CreateVehicleServiceRecordAsync(request);
            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(VehicleServiceRecordUpdateEntity request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ServiceResponse<VehicleServiceRecordEntity>(Constants.ErrorCode, "Girilen bilgileri lütfen kontrol ediniz"));
            }

            var response = await _apiService.UpdateVehicleServiceRecordAsync(request);
            return Json(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteVehicleServiceRecordAsync(id);
            return Json(response);
        }
    }
}
