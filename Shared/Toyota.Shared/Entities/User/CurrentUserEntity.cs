namespace Toyota.Shared.Entities.User
{
    public class CurrentUserEntity
    {

        public long? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? FullName { get; set; }
        public bool IsAdmin { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public string? Ip { get; set; }
        public string? ExpiresDate { get; set; }
        public string? Token { get; set; }
        public string? Channel { get; set; }
        public string? LanguageCode { get; set; } = "tr-TR";
        public string? DeviceId { get; set; } 
        public string? HeaderDeviceId { get; set; }
        public string? TokenType { get; set; }
        public string? FcmToken { get; set; }
    }
}
