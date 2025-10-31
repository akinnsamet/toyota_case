using System.ComponentModel;
using System.Reflection;

namespace Toyota.Shared.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescriptionFromValue(Type enumType, int? value)
        {
            if (!enumType.IsEnum || value == null)
                return "";

            if (!Enum.IsDefined(enumType, value))
                return "";

            var enumValue = (Enum)Enum.ToObject(enumType, value);

            FieldInfo field = enumType.GetField(enumValue.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? enumValue.ToString();
        }
    }
}
