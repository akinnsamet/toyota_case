using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Toyota.Web.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Session.GetString("AccessToken");
            
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadJwtToken(token);

                    var claims = new List<Claim>();
                    foreach (var claim in jsonToken.Claims)
                    {
                        claims.Add(claim);
                    }

                    var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                    context.User = new ClaimsPrincipal(identity);
                }
                catch
                {
                    context.Session.Remove("AccessToken");
                }
            }

            await _next(context);
        }
    }
}
