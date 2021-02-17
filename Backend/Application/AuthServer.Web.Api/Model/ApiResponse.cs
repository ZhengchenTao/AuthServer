using AuthServer.Common.Extensions;
using AuthServer.Exceptions;
using System.Net;

namespace AuthServer.Web.Api.Model
{
    /// <summary>
    /// API响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public ErrorCode Code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// APIResponse
        /// </summary>
        /// <param name="data"></param>
        public ApiResponse(T data)
        {
            Code = ErrorCode.Ok;
            Msg = "OK";
            Data = data;
        }

        /// <summary>
        /// APIResponse
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ApiResponse(ErrorCode code, string message = null)
        {
            Code = code;
            Msg = message ?? code.GetDescription();
        }

        /// <summary>
        /// APIResponse
        /// </summary>
        public ApiResponse() { }
    }
}
