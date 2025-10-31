using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Toyota.Application.Api;
using Toyota.Shared.Language;
using Toyota.Shared.Utilities;

namespace Toyota.Api.Authorization
{
    public class PolicyBasedAuthorizeAttribute : TypeFilterAttribute
    {
        public PolicyBasedAuthorizeAttribute() : base(typeof(PolicyBasedAuthorize))
        {
        }
    };

    public class PolicyBasedAuthorize() : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = ApiConfiguration.CurrentUser;
            if (user.Id == 0)
            {
                context.Result = new JsonResult(new UnauthorizedObjectResult(DataLanguageManager.GetDataTranslation(user.LanguageCode ?? Constants.TRLanguage, "INVALID_TOKEN")));
                return;
            }
            else {
                return;
            }
        }
    }

}
