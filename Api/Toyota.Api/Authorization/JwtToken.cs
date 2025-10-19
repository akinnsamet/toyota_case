using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Toyota.Application.Api;
using Toyota.Entities.User;
using Toyota.Shared.Entities.Enum;
using Toyota.Shared.Entities.User;
using Toyota.Shared.Extensions;
using static Toyota.Shared.Entities.Enum.Enums;

namespace Toyota.Api.Authorization
{
    public static class JwtToken
    {
        public static UserTokenEntity GetToken(LoginResponseUserEntity user, int? expiresMinute=null)
        {
            var iss = ApiConfiguration.Configuration?.GetSection("Audience").GetSection("Iss").Value ?? "";
            var aud = ApiConfiguration.Configuration?.GetSection("Audience").GetSection("Aud").Value ?? "";
            if (expiresMinute == null)
            {
                expiresMinute = ApiConfiguration.Configuration?.GetSection("TokenExpires:Access").Value.ToInt() ?? 0;
            }

            string? xmlKey = File.ReadAllText(@"Authorization//rsa-private-key.xml");
            

            SecurityKey key = JwtKeyHelper.BuildRsaSigningKey(xmlKey);

            DateTime expires = DateTime.UtcNow.AddMinutes(expiresMinute ?? 48);
            var claims = new[]
            {
                    new Claim("UserId", user.Id.ToStr()),
                    new Claim("IsAdmin", user.IsAdmin.ToStr()),
                    new Claim("FullName", user.FullName.ToStr()),
                    new Claim("Expires",expires.ToStr()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = iss,
                Audience = aud,
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(securityToken);
            
            return new UserTokenEntity(){Token = jwtToken,ExpiresDate = expires };
        }
    }
}
