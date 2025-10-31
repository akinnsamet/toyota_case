using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toyota.Shared.Entities.Common;

namespace Toyota.Application.Domain
{
    [Table("VehicleServiceRecordLog")]
    public class VehicleServiceRecordLog : BaseDataModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = null!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
