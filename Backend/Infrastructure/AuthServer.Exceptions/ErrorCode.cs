using System.ComponentModel;

namespace AuthServer.Exceptions
{
    public enum ErrorCode
    {
        Ok = 0,

        #region 1xxx 系统异常
        [Description("未知错误")]
        Unknown = 1000,
        [Description("禁止访问")]
        Forbidden = 1001,
        [Description("排序字段不存在")]
        Sorting = 1002,
        [Description("创建用户失败")]
        CreateUserFailed = 1101,
        #endregion
    }
}
