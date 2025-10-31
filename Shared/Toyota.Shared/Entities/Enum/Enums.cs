using System.ComponentModel;

namespace Toyota.Shared.Entities.Enum
{
    public class Enums
    {
        public enum ApiCallRequestType
        {
            GET = 0,
            POST = 1,
            PUT = 2,
            DELETE = 3
        }
        public enum TableStatus
        {
            Active = 1,
            Deleted = 0,
        }
        public enum TimeTypeEnum
        {
            Day = 1,
            Week = 2,
            Month = 3,
            Year = 4
        }

        public enum LangEnum
        {
            [Description("en")]
            EN = 1,
            [Description("tr")]
            TR = 2,
            [Description("es")]
            ES = 3
        }
        public enum DeviceTypeEnum
        {
            [Description("android")]
            Android = 1,
            [Description("ios")]
            Ios = 2,
            [Description("web")]
            Web = 3
        }
      
    }
}
