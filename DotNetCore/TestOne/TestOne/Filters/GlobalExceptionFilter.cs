using Commons;
using ExceptionManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLog;

namespace TestOne.Filters
{
	public class GlobalExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			Exception ex = context.Exception;
			JsonResultModel resultModel = new JsonResultModel();
			if (ex is ApiException)
			{
				var apiEx = ex as ApiException;
				resultModel.SetCode(apiEx.Code).SetMsg(apiEx.Message);
			}
			else
			{
				resultModel.SetCode(ResultCode.CUSTOMEXCEPTION).SetMsg(ex.Message);
			}
			Logger.Default.Error(context.HttpContext.Request.Path, context.Exception);
			context.Result = new JsonResult(resultModel);
			context.ExceptionHandled = true;
			
		}
	}
}
