using System;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Common.Models
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// 分页页码
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// 分页大小
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 总数[非请求参数]
        /// </summary>
        public int Total { get; set; }
    }
}
