using System.Text.Json.Serialization;
using Toyota.Shared.Entities.Common;

namespace Toyota.Entities.Logs
{
    public class ApplicationLogSearchEntity : SearchEntity
    {
        [JsonPropertyName("Draw")]
        public int? Draw { get; set; }
    }
}
