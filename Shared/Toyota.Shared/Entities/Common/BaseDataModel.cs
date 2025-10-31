using System.ComponentModel.DataAnnotations;
using Toyota.Shared.Entities.Enum;
using Toyota.Shared.Extensions;

namespace Toyota.Shared.Entities.Common
{
    public class BaseDataModel: BaseContextEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
