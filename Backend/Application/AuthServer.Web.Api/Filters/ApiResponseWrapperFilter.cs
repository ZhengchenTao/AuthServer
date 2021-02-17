using AuthServer.Web.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthServer.Web.Api.Filters
{
    /// <summary>
    /// ApiResponse封装
    /// </summary>
    public class ApiResponseWrapperFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Action 结束对结果进行包装
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                if (actionExecutedContext.Result is ObjectResult result)
                {
                    ApiResponse<object> response = new ApiResponse<object>(result.Value);
                    actionExecutedContext.Result = new ObjectResult(response);
                }
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
