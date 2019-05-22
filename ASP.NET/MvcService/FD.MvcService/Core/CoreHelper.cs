using System;
using System.Web.Mvc;

namespace FD.MvcService
{
    public class CoreHelper
    {
        public static FDJsonResult GetJsonResult(object result)
        {
            return new FDJsonResult
            {
                Data = result,
                MaxJsonLength = Int32.MaxValue,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}