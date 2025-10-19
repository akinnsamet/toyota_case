using Toyota.Shared.Entities.Enum;
using Toyota.Shared.Extensions;
using static Toyota.Shared.Entities.Enum.Enums;

namespace Toyota.Business.Common
{
    public abstract class ToyotaBaseBusiness()
    {
        protected readonly short _activeStatus = Enums.TableStatus.Active.ToByte();
        protected readonly short _deleteStatus = TableStatus.Deleted.ToByte();
    }
}
