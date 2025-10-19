using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Toyota.Web.Attributes
{
    public class SessionAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var accessToken = context.HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            await next();
        }
    }
}
