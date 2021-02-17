using AuthServer.Common.Enums;
using AuthServer.Common.Models;
using AuthServer.Exceptions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AuthServer.Common.Extensions
{
    public static partial class Extensions
    {
        public static IQueryable<T> DataSorting<T>(this IQueryable<T> query, SortingInfo sorting = null) where T : class
        {
            if (sorting == null) return query;
            if (string.IsNullOrWhiteSpace(sorting.Sort)) return query;

            return sorting.Direction == SortingDirection.ASC
                ? query.OrderBy(SortingExpression<T>(sorting))
                : query.OrderByDescending(SortingExpression<T>(sorting));
        }

        public static IQueryable<T> DataPaging<T>(this IQueryable<T> query, PagingInfo paging = null) where T : class
        {
            if (paging == null) return query;

            paging.Total = query.Count();
            return query.Skip((paging.PageNo - 1) * paging.PageSize).Take(paging.PageSize);
        }

        private static Expression<Func<T, object>> SortingExpression<T>(SortingInfo sorting)
        {
            var genericType = typeof(T);

            var parameter = Expression.Parameter(genericType, "s");
            var funcType = typeof(Func<,>).MakeGenericType(genericType, typeof(object));

            var lambdaMethod = typeof(Expression)
                .GetMethod("Lambda", 1, new Type[] { typeof(Expression), typeof(ParameterExpression[]) })
                .MakeGenericMethod(funcType);

            Expression exp = parameter;
            Type type = genericType;

            var propertyName = sorting.Sort?.Trim();
            if (string.IsNullOrWhiteSpace(propertyName)) throw new FriendlyException(ErrorCode.Sorting);

            var property = type.GetProperties().FirstOrDefault(x => string.Equals(x.Name, propertyName, StringComparison.OrdinalIgnoreCase));

            if (property == null) throw new FriendlyException(ErrorCode.Sorting);

            type = property.PropertyType;
            exp = Expression.MakeMemberAccess(exp, property);

            var expression = lambdaMethod.Invoke(null, new object[] {
                Expression.Convert(exp, typeof(object)), new ParameterExpression[] { parameter }
            });
            return (Expression<Func<T, object>>)expression;
        }
    }
}
