using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace FD.MvcService
{
    public class FDJsonResult : JsonResult
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
}