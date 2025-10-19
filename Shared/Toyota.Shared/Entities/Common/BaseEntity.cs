using Toyota.Shared.Entities.Enum;
using Toyota.Shared.Extensions;

namespace Toyota.Shared.Entities.Common
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUser { get; set; }
        public short TableStatus { get; set; } = Enums.TableStatus.Active.ToByte();
    }
}
