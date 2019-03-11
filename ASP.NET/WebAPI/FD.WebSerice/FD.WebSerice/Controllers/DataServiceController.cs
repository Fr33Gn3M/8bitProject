using FD.Commons;
using FD.DataBase;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TH.ExceptionManager;

namespace FD.WebSerice.Controllers
{
    public class DataServiceController : Controller
    {
        public string Index()
        {
            return "服务运行中。。。";
        }

        public System.Web.Mvc.JsonResult Query()
        {
            int count = 0;
            var result = new FD.DataBase.JsonResult() { };
            try
            {
                string filter = Encrypt.DecryptJson(Request["filter"]);
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                var objs = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(filters, ref count);
                object[] info = null;
                info = objs;
                result.Rows = info;
                result.Total = count;
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
                    result.Status = MessageTypeCode.Error;
                    result.Error = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult Querys()
        {
            var result = new FD.DataBase.JsonResult() { };
            int count = 0;
            try
            {
                string filters = Encrypt.DecryptJson(Request["filters"]);
                var queryFilters = JsonConvert.DeserializeObject<QueryPageFilter[]>(filters);
                var info = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryFieldResults(queryFilters);
                result.Rows = info;
                result.Total = count;
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
                    result.Status = MessageTypeCode.Error;
                    result.Error = ex.Message;
                }
            }
           return Utility.GetJsonResult(result);
        }
        
        public System.Web.Mvc.JsonResult Update()
        {
            var queryParams = Request;
            var result = new FD.DataBase.JsonResult() { };
            try
            {
                string objs = Encrypt.DecryptJson(queryParams["objs"]);
                string tableName = Encrypt.DecryptJson(queryParams["tb"]);
                string part = Encrypt.DecryptJson(queryParams["part"]);
                bool ispart = false;
                if (!string.IsNullOrEmpty(part) && part == "1")
                    ispart = true;
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(objs);
                ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(tableName, dic, ispart);
                if (ServiceAppContext.Instance.HistroyBaseManage != null)
                    ServiceAppContext.Instance.HistroyBaseManage.updateToHistory(tableName, dic);
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
                    result.Status = MessageTypeCode.Error;
                    result.Error = ex.Message;
                }
            }
           return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult FilterUpdate()
        {
            var queryParams = Request;
            var result = new FD.DataBase.JsonResult() { };

            try
            {
                string objs = Encrypt.DecryptJson(queryParams["objs"]);
                string filter = Encrypt.DecryptJson(queryParams["filter"]);
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(objs);
                ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(dic, filters);
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
                    result.Status = MessageTypeCode.Error;
                    result.Error = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }
        
        public System.Web.Mvc.JsonResult Updates()
        {
            var queryParams = Request;
            var result = new FD.DataBase.JsonResult() { };
            try
            {
                string objs = Encrypt.DecryptJson(queryParams["objs"]);
                string part = Encrypt.DecryptJson(queryParams["part"]);
                bool ispart = false;
                if (!string.IsNullOrEmpty(part) && part == "1")
                    ispart = true;
                var tableList = (JContainer)JsonConvert.DeserializeObject(objs);
                foreach (JToken tables in tableList)
                {
                    var tableInfos = (JProperty)tables;
                    var tableName = tableInfos.Name;
                    var TableFields = FD.Commons.JavaScriptSerializer.Json.Deserialize<Dictionary<string, object>[]>(tableInfos.Value.ToString());
                    ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(tableName, TableFields, ispart);
                    if(ServiceAppContext.Instance.HistroyBaseManage!=null)
                        ServiceAppContext.Instance.HistroyBaseManage.updateToHistory(tableInfos.Name, TableFields);
                }
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
                    result.Status = MessageTypeCode.Error;
                    result.Error = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult Delete()
        {
            var queryParams = Request;
            var result = new DataBase.JsonResult() { };
            string filter = Encrypt.DecryptJson(queryParams["filter"]);
            if (!string.IsNullOrEmpty(filter))
            {
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                ServiceAppContext.Instance.DataBaseClassHelper.DeleteObjects(filters);
            }
            else
            {
                string ids = Encrypt.DecryptJson(queryParams["ids"]);
                string tableName = Encrypt.DecryptJson(queryParams["tb"]);
                var idInfos = JsonConvert.DeserializeObject<string[]>(ids);
                ServiceAppContext.Instance.DataBaseClassHelper.DeleteObjects(tableName, idInfos);
            }
            result.Status = MessageTypeCode.Success;
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult BatchInsert()
        {
            var queryParams = Request;
            var result = new DataBase.JsonResult() { };
            try
            {
                string objs = Encrypt.DecryptJson(queryParams["objs"]);
                string tableName = Encrypt.DecryptJson(queryParams["tb"]);
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(objs);
                ServiceAppContext.Instance.DataBaseClassHelper.BatchInsert(tableName, dic);
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
                    result.Status = MessageTypeCode.Error;
                    result.Error = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }
    }
}
