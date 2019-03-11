using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FD.WebSerice
{
    public class Utility
    {
        public class ExJsonResult : System.Web.Mvc.JsonResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }

                HttpResponseBase response = context.HttpContext.Response;
                if (this.Data != null)
                {
                    JsonSerializerSettings setting = new JsonSerializerSettings();
                    // 设置日期序列化的格式  
                    setting.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    response.Write(JsonConvert.SerializeObject(Data, setting));
                }
            }
        }

        public static ExJsonResult GetJsonResult(object result)
        {
            return new ExJsonResult
            {
                Data = result,
                MaxJsonLength = Int32.MaxValue,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}