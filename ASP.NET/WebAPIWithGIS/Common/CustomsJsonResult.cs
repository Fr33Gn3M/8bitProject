using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace Common
{
    public class CustomsJsonResult : JsonResult
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
        public static CustomsJsonResult GetJsonResult(object result)
        {
            return new CustomsJsonResult
            {
                Data = result,
                MaxJsonLength = Int32.MaxValue,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
