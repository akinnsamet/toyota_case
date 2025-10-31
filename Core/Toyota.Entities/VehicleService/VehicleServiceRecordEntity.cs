
using Toyota.Entities.Locations;

namespace Toyota.Entities.VehicleService
{
    public class VehicleServiceRecordEntity
    {
        public long? Id { get; set; }
        public string LicensePlate { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public int Mileage { get; set; }
        public int? ModelYear { get; set; }
        public DateTime ServiceDate { get; set; }
        public bool? HasWarranty { get; set; }
        public CityEntity? ServiceCity { get; set; }
        public string? ServiceNote { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    }
}
