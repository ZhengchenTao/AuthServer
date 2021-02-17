using AuthServer.Common.Extensions;
using AuthServer.Exceptions;
using AuthServer.Web.Api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace AuthServer.Web.Api.Filters
{
    /// <summary>
    /// 异常封装
    /// </summary>
    public class ExceptionWrapperFilter : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _enviroment;

        public ExceptionWrapperFilter(IWebHostEnvironment enviroment)
        {
            _enviroment = enviroment;
        }

        /// <summary>
        /// 发生异常时进行包装
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception.GetOriginalException();
            ApiResponse<object> response;

            if (exception is FriendlyException)
                response = new ApiResponse<object>(((FriendlyException)exception).Code, exception.Message);
            else if (!_enviroment.IsProduction())
                response = new ApiResponse<object>(ErrorCode.Unknown, exception.Message);
            else
                response = new ApiResponse<object>(ErrorCode.Unknown);

            //TODO: Write log.
            context.Result = new ObjectResult(response);
            return Task.CompletedTask;
        }
    }
}
