using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toyota.Shared.Entities.Common;

namespace Toyota.Application.Domain
{
    [Table("VehicleServiceRecord")]
    public class VehicleServiceRecord : BaseDataModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]


        [Required]
        [MaxLength(20)]
        [Display(Name = "Araç Plakası")]
        public string LicensePlate { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [Display(Name = "Marka Adı")]
        public string BrandName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [Display(Name = "Model Adı")]
        public string ModelName { get; set; } = null!;

        [Required]
        [Display(Name = "KM Bilgisi")]
        public int Mileage { get; set; }

        [Display(Name = "Model Yılı")]
        public int? ModelYear { get; set; }

        [Required]
        [Display(Name = "Servise Geliş Tarihi")]
        public DateTime ServiceDate { get; set; }

        [Display(Name = "Garantisi Var mı?")]
        public bool? HasWarranty { get; set; }

        public long? ServiceCityId { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Servis Notu")]
        public string? ServiceNote { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? DeleteUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long CreateUserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("ServiceCityId")]
        public virtual City? ServiceCity { get; set; }
    }
}
