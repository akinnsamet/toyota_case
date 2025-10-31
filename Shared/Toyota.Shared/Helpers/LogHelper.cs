
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Toyota.Shared.Extensions;

namespace Toyota.Shared.Helpers
{
    public static class LogHelper
    {

        private static readonly HashSet<string> IgnoredProperties = new HashSet<string>
        {
            "UpdateUserId", "LastUpdateDate", "CreateUserId", "CreateDate"
        };

        public static string GenerateChangeLog<T>(T oldEntity, T newEntity,string noChanceDesc,string? prefixDesc)
        {
            try
            {
                if (oldEntity == null || newEntity == null)
                    return string.Empty;

                var type = typeof(T);
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                     .Where(p => !IgnoredProperties.Contains(p.Name));

                var logLines = new List<string>();

                foreach (var prop in properties)
                {
                    var oldValue = prop.GetValue(oldEntity);
                    var newValue = prop.GetValue(newEntity);

                    var oldStr = oldValue?.ToString() ?? "null";
                    var newStr = newValue?.ToString() ?? "null";

                    if (oldStr != newStr)
                    {
                        var displayName = prop.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name;
                        logLines.Add($"{displayName} : '{oldStr}' => '{newStr}'");
                    }
                }
                if (logLines.Count == 0)
                {                    
                    return noChanceDesc;
                }
                if (prefixDesc.IsNotNullOrNotWhiteSpace())
                {
                    logLines.Insert(0,prefixDesc!);
                }

                return string.Join("\n", logLines);
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

    }
}
