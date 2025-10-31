namespace Toyota.Entities.User
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string? FullName { get; set; }
        public bool IsAdmin { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
