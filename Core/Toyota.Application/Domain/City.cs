using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toyota.Shared.Entities.Common;

namespace Toyota.Application.Domain
{
    [Table("City")]
    public class City : BaseDataModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Required]
        [MaxLength(60)]
        public string CityName { get; set; } = null!;
    }
}
