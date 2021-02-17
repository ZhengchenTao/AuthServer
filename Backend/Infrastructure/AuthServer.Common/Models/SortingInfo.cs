using AuthServer.Common.Enums;

namespace AuthServer.Common.Models
{
    public class SortingInfo
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 排序方向 - 默认倒序
        /// </summary>
        public SortingDirection Direction { get; set; }
    }
}
