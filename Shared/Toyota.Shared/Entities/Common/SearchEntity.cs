using System.Text.Json.Serialization;
using Toyota.Shared.Entities.Enum;
using static Toyota.Shared.Entities.Common.Sort;

namespace Toyota.Shared.Entities.Common
{
    public class SearchEntity
    {
        public SearchEntity()
        {
            SortingPaging = new SortingPaging()
            {
                NumberRecords = 10,
                PageNumber = 1,
                SortItem = new()
                {
                    ColumnName = "Id",
                    ColumnOrder = SortOrder.Descending
                }
            };
        }

        [JsonPropertyName("SortingPaging")]
        public SortingPaging? SortingPaging { get; set; }

        [JsonPropertyName("SearchText")]
        public string? SearchText { get; set; }

        [JsonPropertyName("TableStatus")]
        public Enums.TableStatus? TableStatus { get; set; } = Enums.TableStatus.Active;

        [JsonPropertyName("StartDate")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("EndDate")]
        public DateTime? EndDate { get; set; }
    }
}
