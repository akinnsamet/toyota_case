using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toyota.Shared.Entities.Common;

namespace Toyota.Application.Domain
{
    [Table("User")]
    public class User : BaseDataModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = null!;

        [MaxLength(100)]
        public string? FullName { get; set; }

        public bool IsAdmin { get; set; }

        public Guid ExternalId { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
