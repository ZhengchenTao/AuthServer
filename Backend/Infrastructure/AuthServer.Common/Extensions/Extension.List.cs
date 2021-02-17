using AuthServer.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace AuthServer.Common.Extensions
{
    public static partial class Extensions
    {
        public static PaginationList<T> GetPaginationList<T>(this ICollection<T> list, PagingInfo pagingInfo) where T : class
        {
            return new PaginationList<T>()
            {
                List = list,
                Total = pagingInfo == null ? list.Count() : pagingInfo.Total
            };
        }
    }
}
