using System.Text.Json.Serialization;
using Toyota.Shared.Entities.Common;

namespace Toyota.Entities.VehicleService
{
    public class VehicleServiceRecordLogSearchEntity : SearchEntity
    {
        [JsonPropertyName("Draw")]
        public int? Draw { get; set; }
    }
}
