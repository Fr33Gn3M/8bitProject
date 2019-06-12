using FD.DataBase;
using FD.DataBase.Models;
using FD.ExceptionManager;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace FD.MvcService.Controllers
{
    public class DataServiceController : Controller
    {
        // GET: DataService
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Query()
        {
            int count = 0;
            var result = new DataResult() { };
            try
            {
                string filter = Request["filter"];
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                //var objs = ServiceAppContext.Instance.DataBaseHelper.GetQueryResultN(filters, ref count);
                object[] info = null;
                //info = objs;
                //result.Rows = info;
                //result.Total = count;
                result.Status = MessageTypeCode.Success;
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Error = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Error = ex.Message;
                }
            }
            return CoreHelper.GetJsonResult(result);
        }
    }
}