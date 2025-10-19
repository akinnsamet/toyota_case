using System.Linq.Expressions;
using Toyota.Shared.Entities.Common;

namespace Toyota.Shared.Extensions
{
    public static class SortExtensions
    {
        public static IOrderedQueryable<T> ApplySortingPaging<T>(this IOrderedQueryable<T> source, SortingPaging paging)
        {
            if (paging.IsNull())
                return source;

            paging.PageNumber = paging.PageNumber == 0 ? 1 : paging.PageNumber;

            int skip = (paging.PageNumber - 1) * paging.NumberRecords;

            if (paging.SortItem.IsNotNull() && paging.SortItem.ColumnName.IsNotNullOrNotWhiteSpace())
            {
                
                if (paging.SortItem.ColumnOrder == Sort.SortOrder.Ascending)
                    source = ApplyOrder(source,paging.SortItem.ColumnName,"OrderBy");
                else
                    source = ApplyOrder(source, paging.SortItem.ColumnName, "OrderByDescending");

                return (IOrderedQueryable<T>)source.Skip(skip).Take(paging.NumberRecords);
            }
            else
                return (IOrderedQueryable<T>) source.Skip(skip).Take(paging.NumberRecords);
            
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            var props = property.Split('.');
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                string propName = char.ToUpper(prop[0]) + prop.Substring(1);
                var pi = type.GetProperty(propName);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                              && method.IsGenericMethodDefinition
                              && method.GetGenericArguments().Length == 2
                              && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

    }
}
