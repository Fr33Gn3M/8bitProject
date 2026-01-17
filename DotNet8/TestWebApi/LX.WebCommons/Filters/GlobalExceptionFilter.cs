using System.Diagnostics;
using LX.Commons.ExceptionManager;
using LX.Commons.Logs;
using LX.WebCommons.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LX.WebCommons.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            Exception ex = context.Exception;

            //2、获取请求的类名和方法名
            //string strController = filterContext.RouteData.Values["controller"].ToString();
            //string strAction = filterContext.RouteData.Values["action"].ToString();
            //3、记录异常日志
            LogHelper.Current.Error(ex, context.HttpContext.Request.Path + " -> ");
            //4、重定向友好页面
            var jsonResults = new JsonResults() { Msg = ex.Message, Status = 9999 };
            if (ex is ServiceException)
            {
                jsonResults.Status = (ex as ServiceException).Code;
                jsonResults.Msg = (ex as ServiceException).Message;
            }
            var result = new ObjectResult(jsonResults)
            {
                StatusCode = 200
            };
            context.Result = result;
            //5、标记异常已经处理完毕
            context.ExceptionHandled = true;


        }
    }
}
