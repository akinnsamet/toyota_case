namespace Toyota.Entities.User
{
    public class LoginResponseUserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? FullName { get; set; }
        public bool IsAdmin { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    }
}
