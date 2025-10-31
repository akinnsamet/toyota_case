using Toyota.Entities.User;

namespace Toyota.Entities.Auth
{
    public class LoginResponseEntity
    {
        public LoginResponseUserEntity? User { get; set; }
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? TokenExpiry { get; set; }
    }
}
