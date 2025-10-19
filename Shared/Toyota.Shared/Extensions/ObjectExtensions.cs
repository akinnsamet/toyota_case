namespace Toyota.Shared.Extensions
{
    public static class ObjectExtensions
    {

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static void SetPropertyValue(object obj, string propName, object value)
        {
            obj.GetType().GetProperty(propName).SetValue(obj, value, null);
        }

        public static void CopyIfDifferent(Object target, Object source)
        {
            foreach (var prop in target.GetType().GetProperties())
            {
                var targetValue = GetPropValue(target, prop.Name);
                var sourceValue = GetPropValue(source, prop.Name);
                if (!targetValue.Equals(sourceValue) && targetValue!= null)
                {
                    SetPropertyValue(target, prop.Name, sourceValue);
                }
            }
        }
    }
}
