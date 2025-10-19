using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Toyota.Shared.Utilities;
using Toyota.Shared.Entities.User;
using Toyota.Shared.Extensions;

namespace Toyota.Application.Api
{
    public static class ApiConfiguration
    {
        public static IConfiguration? Configuration;
        public static CurrentUserEntity CurrentUser
        {
            get
            {
                var httpContext = new HttpContextAccessor().HttpContext;
                var token = httpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var headerDeviceId = httpContext?.Request?.Headers["device-id"].FirstOrDefault();
                var headerChannel = httpContext?.Request?.Headers["user-agent"].FirstOrDefault()?.Split('/');
                var headerLanguage = httpContext?.Request?.Headers["accept-language"].FirstOrDefault()?.Split(';');
                var selectedLanguage = httpContext?.Request?.Headers["SelectedLanguage"].FirstOrDefault();

                var claims = httpContext?.User.Claims;
                var userId = claims != null ? claims.FirstOrDefault(c => c.Type == "UserId")?.Value : string.Empty;
                var fullName = claims != null ? claims.FirstOrDefault(c => c.Type == "FullName")?.Value : string.Empty;
                var isAdmin = claims != null ? bool.Parse(claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value ?? "false") : false;
                var expiresDate = claims != null ? claims.FirstOrDefault(c => c.Type == "Expires")?.Value : string.Empty;
                var channel = claims != null ? claims.FirstOrDefault(c => c.Type == "Channel")?.Value : string.Empty;
                var languageCode = claims != null ? claims.FirstOrDefault(c => c.Type == "LanguageCode")?.Value : string.Empty;
                var deviceId = claims != null ? claims.FirstOrDefault(c => c.Type == "DeviceId")?.Value : string.Empty;
                var tokenType = claims != null ? claims.FirstOrDefault(c => c.Type == "TokenType")?.Value : string.Empty;

                string? ip = httpContext?.Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                
                var currentUser = new CurrentUserEntity()
                {
                    Id = userId.ToInt(),
                    Ip = ip,
                    ExpiresDate = expiresDate,
                    Token = token,
                    Channel = channel,
                    LanguageCode = Constants.TRLanguage,
                    DeviceId = deviceId,
                    HeaderDeviceId = headerDeviceId,
                    TokenType = tokenType,
                    IsAdmin = isAdmin,
                    FullName = fullName
                };
                return currentUser;
            }

        }
    }
}
