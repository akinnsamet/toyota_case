using System.Text.Json.Serialization;

namespace Toyota.Shared.Entities.Common
{
    public class SortingPaging
    {
        [JsonPropertyName("SortItem")]
        public Sort SortItem { get; set; }

        [JsonPropertyName("PageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("NumberRecords")]
        public int NumberRecords { get; set; }
    }
}
