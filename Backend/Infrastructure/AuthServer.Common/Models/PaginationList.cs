using System.Collections.Generic;

namespace AuthServer.Common.Models
{
    /// <summary>
    /// 分页列表
    /// </summary>
    public class PaginationList<T>
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public IEnumerable<T> List { get; set; }

        /// <summary>
        /// 数据总量
        /// </summary>
        public int Total { get; set; }
    }
}
