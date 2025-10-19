using System.Text.Json.Serialization;

namespace Toyota.Shared.Entities.Common
{
    public class Sort
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum SortOrder
        {
            Ascending,
            Descending
        }

        [JsonPropertyName("ColumnName")]
        public string ColumnName { get; set; }

        [JsonPropertyName("ColumnOrder")]
        public SortOrder ColumnOrder { get; set; }
    }
}
