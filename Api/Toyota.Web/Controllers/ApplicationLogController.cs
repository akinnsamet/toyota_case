using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Toyota.Entities.Logs;
using Toyota.Web.Attributes;
using Toyota.Web.Services;

namespace Toyota.Web.Controllers
{
    [SessionAuthorize]
    public class ApplicationLogController : Controller
    {
        private readonly IApiService _apiService;

        public ApplicationLogController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] ApplicationLogSearchEntity request)
        {
            var response = await _apiService.GetApplicationLogs(request);
            return Json(new
            {
                draw = request.Draw,
                data = response?.ReturnObject ?? [],
                recordsTotal = response?.TotalCount ?? 0,
                recordsFiltered = response?.TotalCount ?? 0
            });
        }

    }
}
