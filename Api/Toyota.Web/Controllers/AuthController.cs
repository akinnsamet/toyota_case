using Microsoft.AspNetCore.Mvc;
using Toyota.Entities.Auth;
using Toyota.Entities.User;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Utilities;
using Toyota.Web.Services;

namespace Toyota.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiService _apiService;

        public AuthController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequestEntity());
        }

        [HttpPost]
        public async Task<IActionResult> LoginAjax([FromBody] LoginRequestEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ServiceResponse<LoginResponseUserEntity>(Constants.ErrorCode, "Lütfen kullanıcı bilgilerini giriniz"));

            var response = await _apiService.LoginAsync(model);

            if (response?.Result?.ResultCode == Constants.SuccessCode && !string.IsNullOrEmpty(response.ReturnObject?.AccessToken))
            {
                HttpContext.Session.SetString("AccessToken", response.ReturnObject.AccessToken);
                HttpContext.Session.SetString("Username", response.ReturnObject.User?.Username ?? "");
                return Ok(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
