namespace Toyota.Shared.Entities.User
{
    public class UserTokenEntity
    {
        public string Token { get; set; }

        public DateTime? ExpiresDate { get; set; }
    }
}
