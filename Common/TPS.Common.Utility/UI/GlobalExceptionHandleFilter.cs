using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Zeus.Common.Helper.Log;

namespace TPS.Common.Utility.UI
{
    /// <summary>
    /// 全局异常处理过滤器
    /// </summary>
    public class GlobalExceptionHandleFilter : IExceptionFilter
    {
        /// <summary>
        /// 在发生异常时调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文。</param>
        public void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;
            FileLogHelper.WriteError(string.Empty, ex, "GlobalExceptionLog");

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { Succeed = false, ReturnValue = "exception", Message = "请求出错，请稍后尝试" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Error",
                    statusCode = (int)HttpStatusCode.InternalServerError
                }));
            }

            filterContext.ExceptionHandled = true;
        }
    }
}
