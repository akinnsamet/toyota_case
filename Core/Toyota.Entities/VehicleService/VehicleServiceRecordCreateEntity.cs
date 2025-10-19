
using System.ComponentModel.DataAnnotations;

namespace Toyota.Entities.VehicleService
{
    public class VehicleServiceRecordCreateEntity
    {
        [Required(ErrorMessage = "Araç plakası gereklidir")]
        [MaxLength(20, ErrorMessage = "Araç plakası en fazla 20 karakter olabilir")]
        [Display(Name = "Araç Plakası")]
        public string LicensePlate { get; set; } = null!;

        [Required(ErrorMessage = "Marka adı gereklidir")]
        [MaxLength(50, ErrorMessage = "Marka adı en fazla 50 karakter olabilir")]
        [Display(Name = "Marka Adı")]
        public string BrandName { get; set; } = null!;

        [Required(ErrorMessage = "Model adı gereklidir")]
        [MaxLength(50, ErrorMessage = "Model adı en fazla 50 karakter olabilir")]
        [Display(Name = "Model Adı")]
        public string ModelName { get; set; } = null!;

        [Required(ErrorMessage = "KM bilgisi gereklidir")]
        [Display(Name = "KM Bilgisi")]
        public int Mileage { get; set; }

        [Display(Name = "Model Yılı")]
        public int? ModelYear { get; set; }

        [Required(ErrorMessage = "Servise geliş tarihi gereklidir")]
        [Display(Name = "Servise Geliş Tarihi")]
        public DateTime ServiceDate { get; set; }

        [Display(Name = "Garantisi Var mı?")]
        public bool? HasWarranty { get; set; }

        [Display(Name = "Servis Şehri")]
        public long? ServiceCityId { get; set; }

        [MaxLength(1000, ErrorMessage = "Servis notu en fazla 1000 karakter olabilir")]
        [Display(Name = "Servis Notu")]
        public string? ServiceNote { get; set; }
        public bool AlreayExists { get; set; } = false;

    }
}
