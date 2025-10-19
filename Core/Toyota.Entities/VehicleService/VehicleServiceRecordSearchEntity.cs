using System.Text.Json.Serialization;
using Toyota.Shared.Entities.Common;

namespace Toyota.Entities.VehicleService
{
    public class VehicleServiceRecordSearchEntity : SearchEntity
    {
        [JsonPropertyName("Draw")]
        public int? Draw { get; set; }
    }
}
